using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Domains
{
    public class TransOthersForListModel
    {
        public int TranOtherID { get; set; }
        public int TimeTrackerId { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal SRP { get; set; }
        public decimal Quantity { get; set; }
        public bool IsPaid { get; set; }
        public decimal Bill { get; set; }
    }

    public class TransMiscellaneousForListModel
    {
        public int TranMisc_Id { get; set; }
        public int TimeTrackerId { get; set; }
        public string TranMiscDescription { get; set; }
        public decimal TranMiscAmount { get; set; }
        public bool IsPaid { get; set; }
        public decimal Bill { get; set; }
    }

    public class TransOthersForCreateUpdate
    {
        public int TranOtherID { get; set; }
        public int TimeTrackerId { get; set; }
        public int ProductID { get; set; }
        public decimal SRP { get; set; }
        public decimal Quantity { get; set; }
        public bool IsPaid { get; set; }
    }

    public class TransMiscellaneousForCreateUpdate
    {
        public int TranMisc_Id { get; set; }
        public int TimeTrackerId { get; set; }
        public string TranMiscDescription { get; set; }
        public decimal TranMiscAmount { get; set; }
        public bool IsPaid { get; set; }
    }

    public class TransBillingSummaryTotalModel
    {
        public string AddOn { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TransBillingModel
    {
        public int TimeTrackerId { get; set; }
        public decimal TotalBill { get; set; }
        public decimal AmountPaid { get; set; }
        public string UserName { get; set; }
    }

    public class ActualUsageSummaryModel
    {
        public DateTime DateTimeIn { get; set; }
        public DateTime DateTimeOut { get; set; }        
        public string ActualDuration { get; set; }
    }
}
