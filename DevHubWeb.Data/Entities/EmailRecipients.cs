using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class EmailRecipients
    {
        public int RecipientId { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
    }
}
