﻿using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class TimeTrackingLogger
    {
        public int TimeTrackerId { get; set; }
        public int BookingId { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public byte? LogStatus { get; set; }
        public string Remarks { get; set; }
        public string UserLoggedBy { get; set; }
        public DateTime LoggedDateTime { get; set; }
        public DateTime? LoggedOutDateTime { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
