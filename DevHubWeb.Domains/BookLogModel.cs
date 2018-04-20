using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevHubWeb.Domains
{
    public class BookLogForCreateModel
    {
        public BookLogForCreateModel()
        {
            this.bookStatus = 0;

        }
        public int BookingID { get; set; }
        [Required]
        public int BookingTypeID { get; set; }

        [Required]
        public int AmenityID { get; set; }

        [Required]
        public int FrequencyID { get; set; }

        public int RateID { get; set; }

        public DateTime DateTimeOfArrival { get; set; }

        public DateTime DateTimeOfDeparture { get; set; }
        public string Remarks { get; set; }
        [Required]
        public string FirstName { get; set; }        
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public string Profession { get; set; }
        public string UserName { get; set; }
        public byte bookStatus { get; set; }        

    }

    public class BookLogForEmailModel
    {
        public int BookingID { get; set; }
        public string FullName { get; set; }
        public DateTime DateTimeOfArrival { get; set; }
        public string Email { get; set; }
        public string AmenityName { get; set; }
        public string ContactNumber { get; set; }
        public decimal RateValue { get; set; }
        public string Capacity { get; set; }
        public string BookingRefCode { get; set; }
        public string Remarks { get; set; }
        public string FrequencyDescription { get; set; }
        public string DatePeriod { get; set; }
        public string Duration { get; set; }
        public string Recipient { get; set; }
    }

    public class BookLogSummaryModel
    {
        public string ClientFullName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AmenityName { get; set; }
        public decimal RateValue { get; set; }
        public string Remarks { get; set; }
        public string LengthPeriod { get; set; }
        public string Duration { get; set; }

    }

    public class BookLogForListModel
    {
        public int BookingID { get; set; }
        public string ClientFullName { get; set; }
        public DateTime DateTimeOfArrival { get; set; }
        public byte BookStatus { get; set; }
        public string Capacity { get; set; }
        public BookLogSummaryModel BookLogSummary { get; set; }
    }

    public class BookLogBlockingScheduleModel
    {
        public DateTime DateTimeOfArrival { get; set; }
        public DateTime DateTimeOfDeparture { get; set; }
    }

    public class BookLogForCreateUpdateReturnModel
    {
        public string FullName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AmenityName { get; set; }
    }
}
