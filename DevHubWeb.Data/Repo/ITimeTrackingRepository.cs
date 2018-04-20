using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo
{
    public interface ITimeTrackingRepository
    {
        Task<int?> DevHub_TimeTracking_Checkin(int bookingId, string userName);
        Task<int?> DevHub_TimeTracking_CheckOut(int timeTrackerID, string userName);
        Task<IEnumerable<TimeTrackingForListModel>> DevHub_TimeTracking_Get(int logStatus, DateTime dtFrom, DateTime dtTo);
        Task<ActualUsageSummaryModel> DevHub_ActualUsageSummary_Get(int timeTrackerID);
    }
}
