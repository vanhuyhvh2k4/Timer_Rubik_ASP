using AutoMapper;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Favorite
            CreateMap<Favorite, GetFavoriteDTO>();
            CreateMap<GetFavoriteDTO, Favorite>();
        }
    }
}
