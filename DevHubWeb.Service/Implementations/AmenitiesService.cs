using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service.Implementations
{
    public class AmenitiesService : IAmenitiesService
    {
        private readonly IAmenitiesRepository _repo;

        public AmenitiesService(IAmenitiesRepository repo)
        {
            this._repo = repo;
        }

        public async Task<bool> AmenityExists(string amenityName)
        {
            return await _repo.AmenityExists(amenityName);
        }

        public async Task<bool> AmenityRateExists(int amenityId, int frequencyID, string capacity)
        {
            return await _repo.AmenityRateExists(amenityId, frequencyID, capacity);
        }

        public async Task<IEnumerable<AmenitiesForListModel>> DevHub_Amenities_Get()
        {
            return await _repo.DevHub_Amenities_Get();
        }

        public async Task<AmenitiesForDetailModel> DevHub_Amenities_Get(int amenityID)
        {
            return await _repo.DevHub_Amenities_Get(amenityID);
        }

        public async Task<AmenityCreatedModel> DevHub_Amenities_Set(AmenitiesForCreateModel model, string userName)
        {
            return await _repo.DevHub_Amenities_Set(model, userName);
        }

        public async Task<IEnumerable<PhotoForReturnModel>> DevHub_AmenityPhotos_Get(int amenityID)
        {
            return await _repo.DevHub_AmenityPhotos_Get(amenityID);
        }

        public async Task<IEnumerable<AmenityRatesForListModel>> DevHub_AmenityRates_Get(int amenityId)
        {
            return await _repo.DevHub_AmenityRates_Get(amenityId);
        }

        public async Task<AmenityRatesForDetailModel> DevHub_AmenityRates_Get(int amenityId, int rateId)
        {
            return await _repo.DevHub_AmenityRates_Get(amenityId, rateId);
        }

        public async Task<int> DevHub_AmenityRates_Set(AmenityRatesForCreateModel model, string userName)
        {
            return await _repo.DevHub_AmenityRates_Set(model, userName);
        }

        public async Task<IEnumerable<AmenityForLookupModel>> GetAmenitiesForLookup()
        {
            return await _repo.GetAmenitiesForLookup();
        }

        public async Task<IEnumerable<FrequenciesForListModel>> GetFrequencies(int frequencyID)
        {
            return await _repo.GetFrequencies(frequencyID);
        }
    }
}
