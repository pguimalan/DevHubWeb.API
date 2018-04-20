using System;

namespace DevHubWeb.Domains
{
    public class EmailParameters
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string DateTimeOfArrival { get; set; }

        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public string Sender { get; set; }
        public string Link { get; set; }

        public bool IsFromDevhub { get; set; }
        public string AmenityName { get; set; }
        public int Fee { get; set; }
        public string Message { get; set; }
        public string ReferenceNumber { get; set; }
        public string ContactNumber { get; set; }
        public string Stay { get; set; }
        public string Period { get; set; }
        public string Duration { get; set; }
        public string Bill { get; set; }
        public string RoomType { get; set; }
        public string GuestCount { get; set; }
        public bool IsAdmin { get; set; }
        public string Time { get; set; }
        public string Rate { get; set; }
    }
}
