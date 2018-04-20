using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo
{
    public interface IEmailRecipientRepository
    {
        Task<List<string>> EmailRecipientList_Get();
    }
}
