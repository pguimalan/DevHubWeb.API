using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransOthersForListModel>> Billing_TransOthers_List(int timeTrackerID);
        Task<IEnumerable<TransMiscellaneousForListModel>> Billing_TransMiscellaneous_List(int timeTrackerID);
        Task<HttpResponseModel> Billing_TransOthers_Remove(int tranOtherID);
        Task<HttpResponseModel> Billing_TransOthers_Set(TransOthersForCreateUpdate model);
        Task<HttpResponseModel> Billing_TransMiscellaneous_Set(TransMiscellaneousForCreateUpdate model);
        Task<HttpResponseModel> Billing_TransMiscellaneous_Remove(int tranMisc_Id);
        Task<IEnumerable<TransBillingSummaryTotalModel>> DevHub_BillingSummary_Get(int timeTrackerID);
        Task<HttpResponseModel> Billing_OverallTransaction_Set(TransBillingModel model);
    }
}
