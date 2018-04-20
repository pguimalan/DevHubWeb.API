using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service
{
    public interface IPhotosService
    {
        Task<PhotoForReturnModel> GetPhoto(int id);
        Task<PhotoForReturnModel> AddPhotoForAmenity(PhotoForCreationModel photoModel);
        Task<int> DeleteAmenityPhoto(PhotoForReturnModel model);
        Task<int> PhotoSetToMain(int amenityID, int photoID);
        Task<bool> CheckIfPhotoExist(int amenityID);
    }
}
