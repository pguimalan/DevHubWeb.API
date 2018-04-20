using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service
{
    public interface ITimeTrackingService
    {
        Task<HttpResponseModel> DevHub_TimeTracking_Checkin(int bookingId, string userName);
        Task<HttpResponseModel> DevHub_TimeTracking_CheckOut(int timeTrackerID, string userName);
        Task<IEnumerable<TimeTrackingForListModel>> DevHub_TimeTracking_Get(int logStatus, DateTime dtFrom, DateTime dtTo);
        Task<ActualUsageSummaryModel> DevHub_ActualUsageSummary_Get(int timeTrackerID);
    }
}
