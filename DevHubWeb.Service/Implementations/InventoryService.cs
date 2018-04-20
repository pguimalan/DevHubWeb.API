using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;

namespace DevHubWeb.Service.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repo;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        public async Task<InventoryReturnModel> CreateUpdate(InventoryModel model, string username)
        {
            return await _repo.CreateUpdate(model, username);
        }

        public async Task<spInventoryModel> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<IEnumerable<spInventoryModel>> GetByProductId(int productId)
        {
            return await _repo.GetByProductId(productId);
        }

        public async Task<IEnumerable<ProductCategoriesForListModel>> GetCategories()
        {
            var list = await _repo.GetCategories();
            return _mapper.Map<IEnumerable<ProductCategoriesForListModel>>(list);
        }

        public async Task<IEnumerable<InventoryProductsModel>> GetInventory(int id)
        {
            return await _repo.GetInventory(id);
        }

        public async Task<IEnumerable<spInventoryModel>> GetInventoryHistory(DateTime dateFrom, DateTime dateTo, bool isSetDefault)
        {
            return await _repo.GetInventoryHistory(dateFrom, dateTo, isSetDefault);
        }

        public async Task<IEnumerable<UnitOfMeasureForListModel>> GetUnitOfMeasure()
        {
            var list = await _repo.GetUnitOfMeasure();
            return _mapper.Map<IEnumerable<UnitOfMeasureForListModel>>(list);
        }
    }
}
