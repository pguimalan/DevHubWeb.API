using AutoMapper;
using DevHubWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class EmailRecipientRepository : IEmailRecipientRepository
    {
        private readonly DevHubContext _context;
        private readonly IMapper _mapper;

        public EmailRecipientRepository(DevHubContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<List<string>> EmailRecipientList_Get()
        {
            var recipients = await _context.EmailRecipients
                           .Where(x => x.IsActive == true)
                           .Select(x => x.EmailAddress)
                           .ToListAsync();

            return recipients;
        }
    }

}
