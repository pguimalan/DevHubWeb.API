using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevHubWeb.Domains
{
    public class AmenitiesForCreateModel
    {
        public AmenitiesForCreateModel()
        {
            this.IsBookable = true;
        }

        public int AmenityId { get; set; }
        [Required]
        public string AmenityName { get; set; }
        [Required]
        public string AmenityDescription { get; set; }
        public bool IsBookable { get; set; }
    }

    public class AmenityCreatedModel
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
    }

    public class AmenityForLookupModel
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
    }

    public class AmenitiesForListModel
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public string AmenityDescription { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsBookable { get; set; }
        public string PhotoURL { get; set; }
        public IEnumerable<AmenityRatesForListModel> Rates { get; set; }
    }

    public class AmenitiesForDetailModel
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public string AmenityDescription { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsBookable { get; set; }
        public IEnumerable<PhotoForReturnModel> Photos { get; set; }
    }

    public class AmenityRatesForCreateModel
    {
        public int RateId { get; set; }
        public int AmenityId { get; set; }
        public int FrequencyId { get; set; }
        public decimal RateValue { get; set; }
        public string Capacity { get; set; }
    }

    public class AmenityRatesForListModel
    {
        public int RateId { get; set; }
        public int AmenityId { get; set; }
        public string FrequencyName { get; set; }
        public decimal RateValue { get; set; }
        public string Capacity { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }

    public class AmenityRatesForDetailModel
    {
        public int RateId { get; set; }
        public int AmenityId { get; set; }
        public string FrequencyName { get; set; }
        public decimal RateValue { get; set; }
        public string Capacity { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }

    public class FrequenciesForListModel
    {
        public int FrequencyId { get; set; }
        public string FrequencyName { get; set; }
        public string FrequencyDescription { get; set; }
        public int FrequencyValue { get; set; }
    }
}
