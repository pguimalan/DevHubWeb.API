using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DevHubWeb.Data.DataHelpers;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using Microsoft.Extensions.Options;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class InventoryRepository : DataManager, IInventoryRepository
    {
        private readonly IOptions<AppSettingsModel> _options;
        private readonly DevHubContext _context;

        public InventoryRepository(IOptions<AppSettingsModel> options, DevHubContext context)
        {
            this._options = options;
            this._context = context;
        }

        public async Task<InventoryReturnModel> CreateUpdate(InventoryModel model, string username)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int?>("dbo.DevHub_AddProducts_Set",
                            new
                            {
                                iIntRecId = model.InventoryId,
                                iIntProduct = model.ProductId,
                                iDecQuantity = model.Quantity,
                                UserName = username
                            },
                            commandType: CommandType.StoredProcedure);


                if (result.FirstOrDefault() > 0)
                {
                    model.Username = username;
                    return new InventoryReturnModel() { Inventory = model, Action = result.FirstOrDefault().Value };
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<spInventoryModel> GetById(int id)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<spInventoryModel>("dbo.DevHub_AddProductsByID_Get",
                    new
                    {
                        iIntRecId = id
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<spInventoryModel>> GetByProductId(int productId)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<spInventoryModel>("dbo.DevHub_AddProductsByProductID_Get",
                    new
                    {
                        iIntProductId = productId
                    },
                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<InvProductCategories>> GetCategories()
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                return await con.QueryAsync<InvProductCategories>("dbo.DevHub_ProductCategories_List", new { }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<InventoryProductsModel>> GetInventory(int id)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<InventoryProductsModel>("dbo.DevHub_QuickInventory_Get",
                    new
                    {
                        iIntProductID = id
                    },
                    commandType: CommandType.StoredProcedure);

                return result;

            }
        }

        public async Task<IEnumerable<spInventoryModel>> GetInventoryHistory(DateTime dateFrom, DateTime dateTo, bool isSetDefault)
        {
            var from = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day).ToString("yyyy-MM-dd");
            var to = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day).ToString("yyyy-MM-dd");

            if (isSetDefault)
            {
                from = DateTime.MinValue.ToString("yyyy-MM-dd");
                to = DateTime.MaxValue.ToString("yyyy-MM-dd");
            }                

            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<spInventoryModel>("dbo.DevHub_AddProducts_Get",
                    new
                    {
                        iDtFrom = from,
                        iDtTo = to
                    },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<IEnumerable<InvUnitOfMeasure>> GetUnitOfMeasure()
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                return await con.QueryAsync<InvUnitOfMeasure>("dbo.DevHub_ProductUOM_List", new { }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
