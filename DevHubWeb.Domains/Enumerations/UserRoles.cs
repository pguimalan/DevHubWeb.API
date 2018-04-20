using DevHubWeb.Libraries;
using DevHubWeb.Libraries.ObjectAttirbutes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Domains.Enumerations
{
    public enum UserRolesEnum : byte
    {
        [StringValue("Admin")]
        Admin = 1,
        [StringValue("User")]
        User = 2,
        [StringValue("Client")]
        Client = 3,
        [StringValue("Super User")]
        SuperUser = 4
    }
}
