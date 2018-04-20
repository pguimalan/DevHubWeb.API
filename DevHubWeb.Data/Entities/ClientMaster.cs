using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class ClientMaster
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Profession { get; set; }
        public string AddedBy { get; set; }
        public DateTime? DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
