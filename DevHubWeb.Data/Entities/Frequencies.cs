using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class Frequencies
    {
        public int FrequencyId { get; set; }
        public string FrequencyName { get; set; }
        public string FrequencyDescription { get; set; }
        public int FrequencyValue { get; set; }
    }
}
