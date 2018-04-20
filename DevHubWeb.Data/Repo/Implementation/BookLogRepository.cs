using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DevHubWeb.Data.DataHelpers;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using Microsoft.Extensions.Options;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class BookLogRepository : DataManager, IBookLogRepository
    {
        private readonly IOptions<AppSettingsModel> _options;
        private readonly DevHubContext _context;

        public BookLogRepository(IOptions<AppSettingsModel> options, DevHubContext context)
        {
            this._options = options;
            this._context = context;
        }

        public async Task<IEnumerable<BookLogForListModel>> BookLogList_Get(string dtFrom, string dtTo)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var forList = await con.QueryAsync<BookLogForListModel>("DevHub_BookLogList_Get",
                   new
                   {
                       @iDtFrom = dtFrom,
                       @iDtTo = dtTo
                   },
                   commandType: CommandType.StoredProcedure);

                foreach(BookLogForListModel m in forList)
                {
                    m.BookLogSummary = await BookLogSummary_Get(m.BookingID);
                }

                return forList;
            }
        }

        private async Task<BookLogSummaryModel> BookLogSummary_Get(int bookingId)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<BookLogSummaryModel>("DevHub_BookLogSummary_Get",
                   new
                   {
                       @iIntBookingID = bookingId
                   },
                   commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<BookLogForEmailModel> BookLog_Set(BookLogForCreateModel model)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<BookLogForEmailModel>("dbo.DevHub_BookLog_Set",
                   new
                   {
                       @iIntBookingID = model.BookingID,
                       @iIntBookingTypeID = model.BookingTypeID,
                       @iIntAmenityID = model.AmenityID,
                       @iIntFrequencyID = model.FrequencyID,
                       @iDtDateTimeOfArrival = model.DateTimeOfArrival,
                       @iDtDateTimeOfDeparture = model.DateTimeOfDeparture,
                       @iStrRemarks = model.Remarks,
                       @iStrFirstName = model.FirstName,
                       @iStrMiddleName = model.MiddleName,
                       @iStrLastName = model.LastName,
                       @iStrEmail = model.Email,
                       @iStrContactNumber = model.ContactNumber,
                       @iStrProfession = model.Profession,
                       @iStrUserName = model.UserName,
                       @iBoolbookStatus = model.bookStatus,
                       @iIntRateID = model.RateID
                   },
                   commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<BookLogBlockingScheduleModel>> BookLogBlockingSchedule_Get(int amenityID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<BookLogBlockingScheduleModel>("dbo.DevHub_AmenitiesBlockingSchedule_Get",
                  new
                  {
                      @iIntAmenityID = amenityID
                  },
                   commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
