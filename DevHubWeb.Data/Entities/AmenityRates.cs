using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class AmenityRates
    {
        public int RateId { get; set; }
        public int AmenityId { get; set; }
        public int FrequencyId { get; set; }
        public decimal RateValue { get; set; }
        public string Capacity { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
