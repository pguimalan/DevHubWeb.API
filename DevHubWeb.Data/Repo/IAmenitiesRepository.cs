using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo
{
    public interface IAmenitiesRepository
    {
        Task<AmenityCreatedModel> DevHub_Amenities_Set(AmenitiesForCreateModel model, string userName);
        Task<IEnumerable<AmenitiesForListModel>> DevHub_Amenities_Get();
        Task<AmenitiesForDetailModel> DevHub_Amenities_Get(int amenityID);
        Task<int> DevHub_AmenityRates_Set(AmenityRatesForCreateModel model, string userName);
        Task<IEnumerable<AmenityRatesForListModel>> DevHub_AmenityRates_Get(int amenityId);
        Task<AmenityRatesForDetailModel> DevHub_AmenityRates_Get(int amenityId, int rateId);
        Task<IEnumerable<PhotoForReturnModel>> DevHub_AmenityPhotos_Get(int amenityID);
        Task<bool> AmenityExists(string amenityName);
        Task<bool> AmenityRateExists(int amenityId, int frequencyID, string capacity);
        Task<IEnumerable<FrequenciesForListModel>> GetFrequencies(int frequencyID);
        Task<IEnumerable<AmenityForLookupModel>> GetAmenitiesForLookup();
    }
}
