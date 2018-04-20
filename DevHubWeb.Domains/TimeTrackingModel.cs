using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Domains
{
    public class TimeTrackingForListModel
    {
        public int TimeTrackerId { get; set; }
        public int BookingID { get; set; }
        public string ClientName { get; set; }
        public string AmenityName { get; set; }
        public string Time_In { get; set; }
        public string Time_Out { get; set; }
        public byte LogStatus { get; set; }
    }
}
