using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class BookLog
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public byte? BookingTypeId { get; set; }
        public Guid? Guid { get; set; }
        public int? AmenityId { get; set; }
        public int? FrequencyId { get; set; }
        public int? RateId { get; set; }
        public DateTime? DateTimeOfArrival { get; set; }
        public DateTime? DateTimeOfDeparture { get; set; }
        public string Remarks { get; set; }
        public byte? BookStatus { get; set; }
        public string BookingRefCode { get; set; }
        public string AddedBy { get; set; }
        public DateTime? DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
