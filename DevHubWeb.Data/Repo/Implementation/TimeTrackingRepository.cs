using Dapper;
using DevHubWeb.Data.DataHelpers;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class TimeTrackingRepository : DataManager, ITimeTrackingRepository
    {
        private readonly IOptions<AppSettingsModel> _options;
        private readonly DevHubContext _context;

        public TimeTrackingRepository(IOptions<AppSettingsModel> options, DevHubContext context)
        {
            this._options = options;
            this._context = context;
        }

        public async Task<ActualUsageSummaryModel> DevHub_ActualUsageSummary_Get(int timeTrackerID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<ActualUsageSummaryModel>("dbo.DevHub_ActualUsageSummary_Get",
                    new
                    {
                        @iIntTimeTrackerID = timeTrackerID
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<int?> DevHub_TimeTracking_Checkin(int bookingId, string userName)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int>("dbo.DevHub_TimeTracking_Checkin",
                    new
                    {
                        @iIntBookingID = bookingId,
                        @iStrUserName = userName
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<int?> DevHub_TimeTracking_CheckOut(int timeTrackerID, string userName)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int>("dbo.DevHub_TimeTracking_CheckOut",
                    new
                    {
                        @iIntTimeTrackerID = timeTrackerID,
                        @iStrUserName = userName
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<TimeTrackingForListModel>> DevHub_TimeTracking_Get(int logStatus, DateTime dtFrom, DateTime dtTo)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<TimeTrackingForListModel>("dbo.DevHub_TimeTracking_Get",
                    new
                    {
                        @iIntLogStatus = logStatus,
                        @iDtFrom = dtFrom,
                        @iDTTo = dtTo
                    },
                    commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
