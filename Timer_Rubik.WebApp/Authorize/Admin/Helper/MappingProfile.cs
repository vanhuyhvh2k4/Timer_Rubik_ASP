using AutoMapper;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, CreateAccountDTO_Admin>();
            CreateMap<CreateAccountDTO_Admin, Account>();

            CreateMap<Account, UpdateAccountDTO_Admin>();
            CreateMap<UpdateAccountDTO_Admin, Account>();
        }
    }
}
