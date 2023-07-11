using AutoMapper;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();

            CreateMap<Rule, RuleDto>();
            CreateMap<RuleDto, Rule>();

            CreateMap<Scramble, ScrambleDto>();
            CreateMap<ScrambleDto, Scramble>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Solve, SolveDto>();
            CreateMap<SolveDto, Solve>();

            CreateMap<Favorite, FavoriteDto>();
            CreateMap<FavoriteDto, Favorite>();
        }
    }
}
