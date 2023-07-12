using AutoMapper;
using Timer_Rubik.WebApp.Authorize.Admin.Dto;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto_AD>();
            CreateMap<AccountDto_AD, Account>();

            CreateMap<Category, CategoryDto_AD>();
            CreateMap<CategoryDto_AD, Category>();

            CreateMap<Scramble, ScrambleDto_AD>();
            CreateMap<ScrambleDto_AD, Scramble>();

            CreateMap<Solve, SolveDto_AD>();
            CreateMap<SolveDto_AD, Solve>();
        }
    }
}
