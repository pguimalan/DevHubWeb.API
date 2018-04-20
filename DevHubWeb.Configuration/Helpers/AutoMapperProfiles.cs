using AutoMapper;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevHubWeb.Configuration.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PhotoForCreationModel, Photos>();
            CreateMap<Amenities, AmenityForLookupModel>();
            CreateMap<Users, UserCreatedModel>();
            CreateMap<UserForRegisterModel, Users>(); 
            CreateMap<Users, UserDetailModel>();
            CreateMap<InvProductCategories, ProductCategoriesForListModel>();
            CreateMap<InvUnitOfMeasure, UnitOfMeasureForListModel>();
            CreateMap<BookLogForEmailModel, BookLogForCreateUpdateReturnModel>();
            
        }
    }
}