using DevHubWeb.Libraries.ObjectAttirbutes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Domains.Enumerations
{
    public enum BookStatusEnum : byte
    {
        [StringValue("Pending")]
        Pending,

        [StringValue("Confirmed")]
        Confirmed,

        [StringValue("Forfeited")]
        Forfeited
    }
}
