using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class BillingOverallTransaction
    {
        public int BillingId { get; set; }
        public int TimeTrackerId { get; set; }
        public decimal? TotalBill { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? Change { get; set; }
        public DateTime? BillingDate { get; set; }
        public string CashierUser { get; set; }
    }
}
