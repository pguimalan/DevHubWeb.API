using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service
{
    public interface IInventoryService
    {
        Task<InventoryReturnModel> CreateUpdate(InventoryModel model, string username);
        Task<spInventoryModel> GetById(int id);
        Task<IEnumerable<spInventoryModel>> GetInventoryHistory(DateTime dateFrom, DateTime dateTo, bool isSetDefault);
        Task<IEnumerable<InventoryProductsModel>> GetInventory(int id);
        Task<IEnumerable<spInventoryModel>> GetByProductId(int productId);
        Task<IEnumerable<ProductCategoriesForListModel>> GetCategories();
        Task<IEnumerable<UnitOfMeasureForListModel>> GetUnitOfMeasure();
    }
}
