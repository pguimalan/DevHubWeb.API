using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;

namespace DevHubWeb.Service.Implementations
{
    public class PhotosService : IPhotosService
    {
        private readonly IPhotosRepository _repo;

        public PhotosService(IPhotosRepository repo)
        {
            this._repo = repo;
        }

        public async Task<PhotoForReturnModel> AddPhotoForAmenity(PhotoForCreationModel photoModel)
        {
            return await _repo.AddPhotoForAmenity(photoModel);
        }

        public async Task<bool> CheckIfPhotoExist(int amenityID)
        {
            return await _repo.CheckIfPhotoExist(amenityID);
        }

        public async Task<int> DeleteAmenityPhoto(PhotoForReturnModel model)
        {
            return await _repo.DeleteAmenityPhoto(model);
        }

        public async Task<PhotoForReturnModel> GetPhoto(int id)
        {
            return await _repo.GetPhoto(id);
        }

        public async Task<int> PhotoSetToMain(int amenityID, int photoID)
        {
            return await _repo.PhotoSetToMain(amenityID, photoID);
        }
    }
}
