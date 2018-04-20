using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;
using DevHubWeb.Service.Methods;

namespace DevHubWeb.Service.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        private readonly HttpResponseService _response;

        public TransactionService(ITransactionRepository repo, HttpResponseService response)
        {
            this._repo = repo;
            this._response = response;
        }

        public async Task<HttpResponseModel> Billing_OverallTransaction_Set(TransBillingModel model)
        {
            var result = await _repo.DevHub_Billing_OverallTransaction_Set(model);
            var modelForReturn = new HttpResponseModel();

            switch (result)
            {
                case -1:
                    modelForReturn.HttpStatusCode = _response.NotFound;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.NotFound);
                    modelForReturn.Remarks = "time tracker not found.";
                    break;

                case -2:
                    modelForReturn.HttpStatusCode = _response.Conflict;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Conflict);
                    modelForReturn.Remarks = "amount paid is less than total bill";
                    break;

                case 1:
                    modelForReturn.HttpStatusCode = _response.Ok;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Ok);
                    modelForReturn.Remarks = "successfully billed out.";
                    break;

                default:
                    modelForReturn.HttpStatusCode = _response.BadRequest;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.BadRequest);
                    modelForReturn.Remarks = "error processing request.";
                    break;
            }

            return modelForReturn;
        }

        public async Task<IEnumerable<TransMiscellaneousForListModel>> Billing_TransMiscellaneous_List(int timeTrackerID)
        {
            return await _repo.Billing_TransMiscellaneous_List(timeTrackerID);
        }

        public async Task<HttpResponseModel> Billing_TransMiscellaneous_Remove(int tranMisc_Id)
        {
            var result = await _repo.Billing_TransMiscellaneous_Remove(tranMisc_Id);
            var modelForReturn = new HttpResponseModel();

            if(result==1)
            {
                modelForReturn.HttpStatusCode = _response.Updated;
                modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Updated);
                modelForReturn.Remarks = "successfully removed.";
                return modelForReturn;
            }

            modelForReturn.HttpStatusCode = _response.BadRequest;
            modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.BadRequest);
            modelForReturn.Remarks = "error processing request.";
            return modelForReturn;

        }

        public async Task<HttpResponseModel> Billing_TransMiscellaneous_Set(TransMiscellaneousForCreateUpdate model)
        {
            var result = await _repo.Billing_TransMiscellaneous_Set(model);
            var modelForReturn = new HttpResponseModel();

            switch (result)
            {
                case 1:
                    modelForReturn.HttpStatusCode = _response.Created;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Created);
                    modelForReturn.Remarks = "successfully added.";
                    break;

                case 2:
                    modelForReturn.HttpStatusCode = _response.Updated;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Updated);
                    modelForReturn.Remarks = "successfully updated.";
                    break;

                default:
                    modelForReturn.HttpStatusCode = _response.BadRequest;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.BadRequest);
                    modelForReturn.Remarks = "error processing request.";
                    break;
            }

            return modelForReturn;
        }

        public async Task<IEnumerable<TransOthersForListModel>> Billing_TransOthers_List(int timeTrackerID)
        {
            return await _repo.Billing_TransOthers_List(timeTrackerID);
        }

        public async Task<HttpResponseModel> Billing_TransOthers_Remove(int tranOtherID)
        {
            var result = await _repo.Billing_TransOthers_Remove(tranOtherID);
            var modelForReturn = new HttpResponseModel();

            if (result == 1)
            {
                modelForReturn.HttpStatusCode = _response.Updated;
                modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Updated);
                modelForReturn.Remarks = "successfully removed.";
                return modelForReturn;
            }

            modelForReturn.HttpStatusCode = _response.BadRequest;
            modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.BadRequest);
            modelForReturn.Remarks = "error processing request.";
            return modelForReturn;
        }

        public async Task<HttpResponseModel> Billing_TransOthers_Set(TransOthersForCreateUpdate model)
        {
            var result = await _repo.Billing_TransOthers_Set(model);
            var modelForReturn = new HttpResponseModel();

            switch (result)
            {
                case 1:
                    modelForReturn.HttpStatusCode = _response.Created;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Created);
                    modelForReturn.Remarks = "successfully added.";
                    break;

                case 2:
                    modelForReturn.HttpStatusCode = _response.Updated;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.Updated);
                    modelForReturn.Remarks = "successfully updated.";
                    break;

                default:
                    modelForReturn.HttpStatusCode = _response.BadRequest;
                    modelForReturn.ResponseModel = _response.ShowHttpResponse(_response.BadRequest);
                    modelForReturn.Remarks = "error processing request.";
                    break;
            }

            return modelForReturn;
        }

        public async Task<IEnumerable<TransBillingSummaryTotalModel>> DevHub_BillingSummary_Get(int timeTrackerID)
        {
            return await _repo.DevHub_BillingSummary_Get(timeTrackerID);
        }
    }
}
