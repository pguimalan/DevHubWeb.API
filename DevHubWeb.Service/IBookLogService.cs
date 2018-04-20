using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service
{
    public interface IBookLogService
    {
        Task<BookLogForCreateUpdateReturnModel> BookLog_Set(BookLogForCreateModel model, string uri);
        Task<IEnumerable<BookLogForListModel>> BookLogList_Get(string dtFrom, string dtTo);
        Task<IEnumerable<BookLogBlockingScheduleModel>> BookLogBlockingSchedule_Get(int amenityID);
    }
}
