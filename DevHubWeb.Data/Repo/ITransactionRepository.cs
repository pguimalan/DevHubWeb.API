using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransOthersForListModel>> Billing_TransOthers_List(int timeTrackerID);
        Task<IEnumerable<TransMiscellaneousForListModel>> Billing_TransMiscellaneous_List(int timeTrackerID);
        Task<int?> Billing_TransOthers_Remove(int tranOtherID);
        Task<int?> Billing_TransOthers_Set(TransOthersForCreateUpdate model);
        Task<int?> Billing_TransMiscellaneous_Set(TransMiscellaneousForCreateUpdate model);
        Task<int?> Billing_TransMiscellaneous_Remove(int tranMisc_Id);
        Task<IEnumerable<TransBillingSummaryTotalModel>> DevHub_BillingSummary_Get(int timeTrackerID);
        Task<int?> DevHub_Billing_OverallTransaction_Set(TransBillingModel model);

    }
}
