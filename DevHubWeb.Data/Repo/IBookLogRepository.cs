using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo
{
    public interface IBookLogRepository
    {
        Task<BookLogForEmailModel> BookLog_Set(BookLogForCreateModel model);
        Task<IEnumerable<BookLogForListModel>> BookLogList_Get(string dtFrom, string dtTo);
        Task<IEnumerable<BookLogBlockingScheduleModel>> BookLogBlockingSchedule_Get(int amenityID);
    }
}
