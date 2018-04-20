using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class BillingTransMiscellaneous
    {
        public int TranMiscId { get; set; }
        public int? TimeTrackerId { get; set; }
        public string TranMiscDescription { get; set; }
        public decimal TranMiscAmount { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsActive { get; set; }
    }
}
