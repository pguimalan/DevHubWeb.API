using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevHubWeb.Domains;
using DevHubWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevHubWeb.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]    
    public class AmenitiesController : BaseController
    {
        private readonly IAmenitiesService _service;

        public AmenitiesController(IAmenitiesService service)
        {
            this._service = service;
        }

        [HttpGet("AmenitiesForLookupGet")]        
        public async Task<IActionResult> AmenitiesForLookupGet()
        {
            var amenities = await _service.GetAmenitiesForLookup();
            
            return Ok(amenities);
        }

        [HttpGet("AmenitiesGet")]        
        public async Task<IActionResult> AmenitiesGet()
        {
            var amenities = await _service.DevHub_Amenities_Get();
            foreach (AmenitiesForListModel am in amenities)
            {
                am.Rates = await _service.DevHub_AmenityRates_Get(am.AmenityId);
            }
            
            return Ok(new { data = amenities });
        }

        [HttpGet("GetFrequencies/{id}")]
        public async Task<IActionResult> GetFrequencies(int id)
        {
            var modelForList = await _service.GetFrequencies(id);

            if (modelForList == null || modelForList.Count() <= 0)
                return StatusCode((int)HttpStatusCode.NotFound, new { data = "No Record Found." });

            return Ok(modelForList);
        }

        [HttpGet("AmenitiesGet/{id}")]
        public async Task<IActionResult> AmenityGetByID(int id)
        {
            var amenities = await _service.DevHub_Amenities_Get(id);

            if (amenities == null)
                return StatusCode((int)HttpStatusCode.NotFound, new { data = "No Record Found." });

            amenities.Photos = await _service.DevHub_AmenityPhotos_Get(id);

            return Ok(new { data = amenities });
        }
                
        [HttpPost("CreateNewAmenity"), Authorize(Roles = "Admin, Super User")]
        public async Task<IActionResult> CreateNewAmenity([FromBody] AmenitiesForCreateModel model)
        {
            if (await _service.AmenityExists(model.AmenityName))
                ModelState.AddModelError("AmenityName", "AmenityName is already taken.");

            // validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var amenityToCreate = await _service.DevHub_Amenities_Set(model, _baseUserName);


            if (amenityToCreate.AmenityId == 0)
                return BadRequest();
            else if (amenityToCreate.AmenityId == -1)
                return StatusCode(409, new { result = "already exist." });

            return Ok(amenityToCreate);
        }
                
        [HttpPost("UpdateAmenity")]
        [Authorize(Roles = "Admin, Super User")]
        public async Task<IActionResult> UpdateAmenity([FromBody] AmenitiesForCreateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var checkAmenity = _service.DevHub_Amenities_Get(model.AmenityId);
            if (checkAmenity == null)
                return NotFound();

            var amenityToBeUpdated = await _service.DevHub_Amenities_Set(model, _baseUserName);
            if (amenityToBeUpdated.AmenityId != model.AmenityId)
                return BadRequest();

            else if (amenityToBeUpdated.AmenityId == -2)
                return StatusCode(409, new { result = "cannot update to existing amenity name." });

            return NoContent();
        }

        [HttpGet("AmenityRatesGet/{amenityID}")]
        public async Task<IActionResult> AmenityRatesGet(int amenityID)
        {
            var amenityRatesList = await _service.DevHub_AmenityRates_Get(amenityID);
            if (amenityRatesList == null)
                return NotFound();

            return Ok(new { data = amenityRatesList });
        }

        [HttpGet("AmenityRatesGet/{amenityID}/{rateID}")]
        public async Task<IActionResult> AmenityRatesGet(int amenityID, int rateID)
        {
            var amenityRatesList = await _service.DevHub_AmenityRates_Get(amenityID, rateID);
            if (amenityRatesList == null)
                return NotFound();

            return Ok(new { data = amenityRatesList });
        }

        [HttpPost("AmenityRatesInsert")]
        [Authorize(Roles = "Admin, Super User")]
        public async Task<IActionResult> AmenityRatesInsert([FromBody] AmenityRatesForCreateModel model)
        {
            if (await _service.AmenityRateExists(model.AmenityId, model.FrequencyId, model.Capacity))
                ModelState.AddModelError("AmenityRate", "Rate is already defined.");

            // validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var amenityRateToCreate = await _service.DevHub_AmenityRates_Set(model, _baseUserName);
            if (amenityRateToCreate == 0)
                return BadRequest();

            return NoContent();
        }

        [HttpPost("AmenityRatesUpdate")]
        [Authorize(Roles = "Admin, Super User")]
        public async Task<IActionResult> AmenityRatesUpdate([FromBody] AmenityRatesForCreateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chkRate = await _service.DevHub_AmenityRates_Get(model.AmenityId, model.RateId);
            if (chkRate == null)
                return NotFound();

            var amenityRateToUpdate = await _service.DevHub_AmenityRates_Set(model, _baseUserName);
            if (amenityRateToUpdate == 0)
                return BadRequest();

            return NoContent();
        }
    }
}