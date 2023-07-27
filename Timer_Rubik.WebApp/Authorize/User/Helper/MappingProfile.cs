using AutoMapper;
using Timer_Rubik.WebApp.Authorize.User.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Account

            
            //Favorite
            CreateMap<Favorite, CreateFavoriteDTO_User>();
            CreateMap<CreateFavoriteDTO_User, Favorite>();

            //Scramble
        }
    }
}
