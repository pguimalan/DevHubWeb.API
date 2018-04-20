using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dapper;
using DevHubWeb.Data.DataHelpers;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class PhotosRepository : DataManager, IPhotosRepository
    {
        private readonly DevHubContext _context;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IOptions<AppSettingsModel> _options;
        private readonly IMapper _mapper;
        private Cloudinary _cloudinary;

        public PhotosRepository(DevHubContext context, IOptions<CloudinarySettings> cloudinaryConfig, IMapper mapper, IOptions<AppSettingsModel> options)
        {
            this._context = context;
            this._cloudinaryConfig = cloudinaryConfig;
            this._options = options;
            this._mapper = mapper;

            Account acc = new Account(_cloudinaryConfig.Value.CloudName,
              _cloudinaryConfig.Value.ApiKey,
              _cloudinaryConfig.Value.ApiSecret);

            this._cloudinary = new Cloudinary(acc);
        }

        public async Task<PhotoForReturnModel> AddPhotoForAmenity(PhotoForCreationModel photoModel)
        {
            var file = photoModel.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoModel.PhotoURL = uploadResult.Uri.ToString();
            photoModel.PublicID = uploadResult.PublicId;

            var photo = _mapper.Map<Photos>(photoModel);


            if (!await CheckIfPhotoExist(photoModel.ReferenceID))
                photo.IsMain = true;
            else photo.IsMain = false;

            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            return _mapper.Map<PhotoForReturnModel>(photo);
        }

        public async Task<bool> CheckIfPhotoExist(int amenityID)
        {
            if (await _context.Photos.AnyAsync(x => x.ReferenceId == amenityID))
                return true;
            return false;
        }

        public async Task<int> DeleteAmenityPhoto(PhotoForReturnModel model)
        {
            var photoToDelete = _context.Photos.SingleOrDefault(p => p.PhotoId == model.PhotoID);

            if (model.PublicID != null)
            {
                var delParams = new DeletionParams(model.PublicID);
                var result = _cloudinary.Destroy(delParams);

                if (result.Result == "ok")
                    _context.Remove(photoToDelete);
            }

            if (model.PublicID == null)
                _context.Photos.Remove(photoToDelete);

            return await _context.SaveChangesAsync();
        }

        public async Task<PhotoForReturnModel> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.PhotoId == id);
            return _mapper.Map<PhotoForReturnModel>(photo);
        }

        public async Task<int> PhotoSetToMain(int amenityID, int photoID)
        {
            using (var con = GetDbConnection(_options.Value.DevHubDBConn))
            {
                var result = await con.QueryAsync<int>("dbo.DevHub_PhotoSetToMain_Set",
                    new
                    {
                        @iIntAmenityID = amenityID,
                        @iIntPhotoID = photoID
                    },
                    commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }
    }
}
