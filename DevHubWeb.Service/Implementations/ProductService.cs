using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;
using DevHubWeb.Service.Methods;

namespace DevHubWeb.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly HttpResponseService _response;

        public ProductService(IProductRepository repo, HttpResponseService response)
        {
            this._repo = repo;
            this._response = response;
        }

        public async Task<HttpResponseModel> CreateUpdate(ProductModel model)
        {
            var result = await _repo.CreateUpdate(model);
            var modelForReturn = new HttpResponseModel();

            switch (result.Action)
            {
                case 1:
                    modelForReturn.HttpStatusCode = _response.Created;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Created);
                    modelForReturn.Remarks = "product created";
                    break;

                case 2:
                    modelForReturn.HttpStatusCode = _response.Updated;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Updated);
                    modelForReturn.Remarks = "product updated";
                    break;

                case -1:
                    modelForReturn.HttpStatusCode = _response.Conflict;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Conflict);
                    modelForReturn.Remarks = "already exist";
                    break;
            }

            return modelForReturn;
        }

        public async Task<IEnumerable<spProductModel>> Get(string query)
        {
            return await _repo.Get(query);
        }

        public async Task<spProductModel> GetById(int id)
        {
            return await _repo.GetById(id);
        }
    }
}
