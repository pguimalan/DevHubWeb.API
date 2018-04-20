using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles = "Admin, User, Super User")]
    public class PhotosController : Controller
    {
        private readonly IAmenitiesService _amenityService;
        private readonly IPhotosService _photoService;

        public PhotosController(IAmenitiesService amenityService, IPhotosService photoService)
        {
            this._amenityService = amenityService;
            this._photoService = photoService;
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _photoService.GetPhoto(id);
            if (photoFromRepo == null)
                return NotFound("Photo Not Found!");
            return Ok(photoFromRepo);
        }

        [HttpPost("AddPhotoForAmenity")]
        public async Task<IActionResult> AddPhotoForAmenity(PhotoForCreationModel photoModel)
        {
            var amenity = await _amenityService.DevHub_Amenities_Get(photoModel.ReferenceID);

            if (amenity == null)
                return NotFound("Could not find amenity.");

            var result = await _photoService.AddPhotoForAmenity(photoModel);

            if (result != null)
                return CreatedAtRoute("GetPhoto", new { id = result.PhotoID }, result);

            return BadRequest("Could not add the photo.");
        }

        [HttpDelete("{photoID}/delete")]
        public async Task<IActionResult> DeleteAmenityPhoto(int photoID)
        {
            var photoFromRepo = await _photoService.GetPhoto(photoID);

            if (photoFromRepo == null)
                return NotFound();

            if (photoFromRepo.IsMain)
                return BadRequest("You cannot delete the main photo.");

            if (await _photoService.DeleteAmenityPhoto(photoFromRepo) > 0)
                return Ok("Photo deleted.");

            return BadRequest("Failed to delete the photo.");

        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int amenityID, int id)
        {
            var result = await _photoService.PhotoSetToMain(amenityID, id);

            if (result == 99)
                return NotFound();

            if (result == 98)
                return BadRequest("This is already the main photo.");

            if (result == 1)
                return NoContent();

            return BadRequest("Could not set photo to main.");
        }
    }
}