using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class BillingTransOthers
    {
        public int TranOtherId { get; set; }
        public int TimeTrackerId { get; set; }
        public int ProductId { get; set; }
        public decimal? Srp { get; set; }
        public decimal? Quantity { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsActive { get; set; }
    }
}
