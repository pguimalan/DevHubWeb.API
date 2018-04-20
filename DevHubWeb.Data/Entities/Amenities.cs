using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class Amenities
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public string AmenityDescription { get; set; }
        public bool? IsBookable { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
