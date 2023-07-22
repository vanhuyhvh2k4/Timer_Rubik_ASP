using AutoMapper;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Account
            CreateMap<Account, GetAccountDTO_Admin>();
            CreateMap<GetAccountDTO_Admin, Account>();

            CreateMap<Account, CreateAccountDTO_Admin>();
            CreateMap<CreateAccountDTO_Admin, Account>();

            CreateMap<Account, UpdateAccountDTO_Admin>();
            CreateMap<UpdateAccountDTO_Admin, Account>();

            //Category
            CreateMap<Category, CreateCategoryDTO_Admin>();
            CreateMap<CreateCategoryDTO_Admin, Category>();

            CreateMap<Category, UpdateCategoryDTO_Admin>();
            CreateMap<UpdateCategoryDTO_Admin, Category>();

            //Solve
            CreateMap<Solve, GetSolveDTO_Admin>();
            CreateMap<GetSolveDTO_Admin, Solve>();

            CreateMap<Solve, CreateSolveDTO_Admin>();
            CreateMap<CreateSolveDTO_Admin, Solve>();

            CreateMap<Solve, UpdateSolveDTO_Admin>();
            CreateMap<UpdateSolveDTO_Admin, Solve>();
        }
    }
}
