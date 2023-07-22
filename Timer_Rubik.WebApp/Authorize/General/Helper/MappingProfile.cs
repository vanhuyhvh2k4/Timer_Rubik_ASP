using AutoMapper;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Helper
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

            //category 
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<GetCategoryDTO, Category>();

            //Favorite
            CreateMap<Favorite, GetFavoriteDTO>();
            CreateMap<GetFavoriteDTO, Favorite>();

            CreateMap<Favorite, CreateFavoriteDTO>();
            CreateMap<CreateFavoriteDTO, Favorite>();

            CreateMap<Favorite, UpdateFavoriteDTO>();
            CreateMap<UpdateFavoriteDTO, Favorite>();

            //Scramble
            CreateMap<Scramble, GetScrambleDTO>();
            CreateMap<GetScrambleDTO, Scramble>();

            CreateMap<Scramble, CreateScrambleDTO>();
            CreateMap<CreateScrambleDTO, Scramble>();

            CreateMap<Scramble, UpdateScrambleDTO>();
            CreateMap<UpdateScrambleDTO, Scramble>();

            //Solve
            CreateMap<Solve, GetSolveDTO>();
            CreateMap<GetSolveDTO, Solve>();

            CreateMap<Solve, CreateSolveDTO>();
            CreateMap<CreateSolveDTO, Solve>();

            CreateMap<Solve, UpdateSolveDTO>();
            CreateMap<UpdateSolveDTO, Solve>();
        }
    }
}
