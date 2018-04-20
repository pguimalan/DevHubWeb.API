using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo
{
    public interface IProductRepository
    {
        Task<ProductReturnModel> CreateUpdate(ProductModel model);
        Task<IEnumerable<spProductModel>> Get(string query);
        Task<spProductModel> GetById(int id);
    }
}
