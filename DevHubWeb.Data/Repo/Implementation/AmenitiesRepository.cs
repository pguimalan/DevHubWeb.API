using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using DevHubWeb.Data.DataHelpers;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class AmenitiesRepository : DataManager, IAmenitiesRepository
    {
        private readonly IOptions<AppSettingsModel> _options;
        private readonly DevHubContext _context;
        private readonly IMapper _mapper;

        public AmenitiesRepository(IOptions<AppSettingsModel> options, DevHubContext context, IMapper mapper)
        {
            this._options = options;
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<bool> AmenityExists(string amenityName)
        {
            if (await _context.Amenities.AnyAsync(x => x.AmenityName == amenityName))
                return true;
            return false;
        }

        public async Task<bool> AmenityRateExists(int amenityId, int frequencyID, string capacity)
        {
            if (await _context.AmenityRates.AnyAsync(x => x.FrequencyId == frequencyID && x.AmenityId == amenityId && x.Capacity == capacity))
                return true;
            return false;
        }

        public async Task<IEnumerable<AmenitiesForListModel>> DevHub_Amenities_Get()
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<AmenitiesForListModel>("dbo.DevHub_Amenities_Get",
                                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<AmenitiesForDetailModel> DevHub_Amenities_Get(int amenityID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<AmenitiesForDetailModel>("dbo.DevHub_Amenities_Get",
                                    new
                                    {
                                        @iIntAmenityID = amenityID
                                    },
                                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<AmenityCreatedModel> DevHub_Amenities_Set(AmenitiesForCreateModel model, string userName)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int>("dbo.DevHub_Amenities_Set",
                    new
                    {
                        @iIntAmenityID = model.AmenityId,
                        @iStrAmenityName = model.AmenityName,
                        @iStrAmenityDescription = model.AmenityDescription,
                        @iBitIsBookable = model.IsBookable,
                        @iStrUserName = userName,
                    },
                    commandType: CommandType.StoredProcedure);

                if (result.FirstOrDefault() > 0)
                {
                    var amenityForReturn = await DevHub_Amenities_Get(result.FirstOrDefault());
                    return new AmenityCreatedModel
                    {
                        AmenityId = amenityForReturn.AmenityId,
                        AmenityName = amenityForReturn.AmenityName
                    };
                }

                return new AmenityCreatedModel
                {
                    AmenityId = 0,
                    AmenityName = string.Empty
                };
            }
        }

        public async Task<IEnumerable<PhotoForReturnModel>> DevHub_AmenityPhotos_Get(int amenityID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<PhotoForReturnModel>("dbo.DevHub_AmenityPhotos_Get",
                                    new
                                    {
                                        @iIntAmenityID = amenityID
                                    },
                                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<IEnumerable<AmenityRatesForListModel>> DevHub_AmenityRates_Get(int amenityId)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<AmenityRatesForListModel>("dbo.DevHub_AmenityRates_Get",
                                    new
                                    {
                                        @iIntRateID = 0,
                                        @iIntAmenityID = amenityId
                                    },
                                    commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<AmenityRatesForDetailModel> DevHub_AmenityRates_Get(int amenityId, int rateId)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<AmenityRatesForDetailModel>("dbo.DevHub_AmenityRates_Get",
                                    new
                                    {
                                        @iIntRateID = rateId,
                                        @iIntAmenityID = amenityId
                                    },
                                    commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<int> DevHub_AmenityRates_Set(AmenityRatesForCreateModel model, string userName)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int>("dbo.DevHub_AmenityRates_Set",
                    new
                    {
                        @iIntRateID = model.RateId,
                        @iIntAmenityID = model.AmenityId,
                        @iIntFrequencyID = model.FrequencyId,
                        @iDecRateValue = model.RateValue,
                        @iStrCapacity = model.Capacity,
                        @iStrUserName = userName
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<AmenityForLookupModel>> GetAmenitiesForLookup()
        {
            var amenitiesforLookUp = await _context.Amenities.ToListAsync();
            return _mapper.Map<IEnumerable<AmenityForLookupModel>>(amenitiesforLookUp);
        }

        public async Task<IEnumerable<FrequenciesForListModel>> GetFrequencies(int frequencyID)
        {
            var modelForList = await _context.Frequencies.Where(f => f.FrequencyId == frequencyID || frequencyID == 0).ToListAsync();
            var modelForReturn = _mapper.Map<IEnumerable<FrequenciesForListModel>>(modelForList);
            return modelForReturn;
        }
    }
}
