using AutoMapper;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Helper.Client
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //account
            CreateMap<Account, RegisterRequest>();
            CreateMap<RegisterRequest, Account>();

            CreateMap<Account, GetAccountDTO>();
            CreateMap<GetAccountDTO, Account>();

            CreateMap<Account, UpdateAccountDTO>();
            CreateMap<UpdateAccountDTO, Account>();

            //scramble
            CreateMap<Scramble, CreateScrambleDTO>();
            CreateMap<CreateScrambleDTO, Scramble>();

            CreateMap<Scramble, UpdateScrambleDTO>();
            CreateMap<UpdateScrambleDTO, Scramble>();

            //category
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<GetCategoryDTO, Category>();

            //Favorite
            CreateMap<Favorite, CreateFavoriteDTO>();
            CreateMap<CreateFavoriteDTO, Favorite>();
        }
    }
}
