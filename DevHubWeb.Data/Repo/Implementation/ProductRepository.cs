using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using DevHubWeb.Data.DataHelpers;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using Microsoft.Extensions.Options;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class ProductRepository : DataManager, IProductRepository
    {
        private readonly IOptions<AppSettingsModel> _options; 
        private readonly IMapper _mapper;
        private readonly DevHubContext _context;

        public ProductRepository(IOptions<AppSettingsModel> options, IMapper mapper, DevHubContext context)
        {
            this._options = options;
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<ProductReturnModel> CreateUpdate(ProductModel model)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<ProductSetReturnModel>("dbo.DevHub_ProductDesc_Set",
                    new
                    {
                        iIntProdId = model.ProductId,
                        iIntCategoryID = model.CategoryId,
                        iStrProductDescription = model.Description,
                        iStrProductName = model.Name,
                        iDecSRP = model.Price,
                        iIntUom_Id = model.UnitMeasure
                    },
                    commandType: CommandType.StoredProcedure);
                model.ProductId = result.FirstOrDefault().ProductID;
                return new ProductReturnModel() { Product = model, Action = result.FirstOrDefault().Result };
            }
        }

        public async Task<IEnumerable<spProductModel>> Get(string query)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<spProductModel>("DevHub_ProductDescList_Get",
                new
                {
                    @iStrSearch = query ?? ""
                },
                commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<spProductModel> GetById(int id)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<spProductModel>("DevHub_ProductDescByID_Get",
                    new
                    {
                        @iIntProductID = id
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }
    }
}
