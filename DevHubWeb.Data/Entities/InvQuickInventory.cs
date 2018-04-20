using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class InvQuickInventory
    {
        public int QuickInvId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
