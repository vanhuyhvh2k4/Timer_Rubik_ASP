using AutoMapper;
using Timer_Rubik.WebApp.Authorize.User.Dto;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Favorite, FavoriteDto_U>();
            CreateMap<FavoriteDto_U, Favorite>();

            CreateMap<Account, UpdateAccountDto_U>();
            CreateMap<UpdateAccountDto_U, Account>();
        }
    }
}
