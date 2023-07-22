using AutoMapper;
using Timer_Rubik.WebApp.Authorize.User.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, GetAccountDTO_User>();
            CreateMap<GetAccountDTO_User, Account>();

            CreateMap<Account, UpdateAccountDTO_User>();
            CreateMap<UpdateAccountDTO_User, Account>();

            CreateMap<Favorite, CreateFavoriteDTO_User>();
            CreateMap<CreateFavoriteDTO_User, Favorite>();

            CreateMap<Favorite, UpdateFavoriteDTO_User>();
            CreateMap<UpdateFavoriteDTO_User, Favorite>();
        }
    }
}
