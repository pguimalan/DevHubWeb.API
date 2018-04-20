using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;
using DevHubWeb.Service.Methods;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DevHubWeb.Service.Implementations
{
    public class BookLogService : IBookLogService
    {
        private readonly IBookLogRepository _repo;
        private readonly IOptions<AppSettingsModel> _options;
        private readonly SendEmailService _send;
        private readonly IEmailRecipientRepository _emails;
        private readonly IMapper _mapper;

        public BookLogService(IBookLogRepository repo, IOptions<AppSettingsModel> options, SendEmailService send, IEmailRecipientRepository emails, IMapper mapper)
        {
            this._repo = repo;
            this._options = options;
            this._send = send;
            this._emails = emails;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<BookLogBlockingScheduleModel>> BookLogBlockingSchedule_Get(int amenityID)
        {
            return await _repo.BookLogBlockingSchedule_Get(amenityID);
        }

        public async Task<IEnumerable<BookLogForListModel>> BookLogList_Get(string dtFrom, string dtTo)
        {
            return await _repo.BookLogList_Get(dtFrom, dtTo);
        }

        public async Task<BookLogForCreateUpdateReturnModel> BookLog_Set(BookLogForCreateModel model, string uri)
        {
            var result = await _repo.BookLog_Set(model);

            var emailRecipients = await _emails.EmailRecipientList_Get();


            if (result != null)
            {
                if(model.BookingTypeID == 3)
                {

                    if (model.AmenityID == 3 || model.AmenityID == 4)
                    {
                        foreach (var recipient in emailRecipients)
                        {
                            result.Recipient = recipient;
                            await _send.SendEmail(_send.BookLogForEmailParam(result, "Add-Admin-Booking-Template", uri, true));
                        }
                        await _send.SendEmail(_send.BookLogForEmailParam(result, "Confirm-Booking-Conference-Template", uri, false));
                    }
                    else
                    {
                        foreach (var recipient in emailRecipients)
                        {
                            result.Recipient = recipient;
                            await _send.SendEmail(_send.BookLogForEmailParam(result, "Add-Admin-Booking-Template", uri, true));
                        }                        
                        await _send.SendEmail(_send.BookLogForEmailParam(result, "Confirm-Booking-Template", uri, false));
                    }
                }                
            }

            var modelForReturn = _mapper.Map<BookLogForCreateUpdateReturnModel>(result);

            return modelForReturn;
        }
    }
}
