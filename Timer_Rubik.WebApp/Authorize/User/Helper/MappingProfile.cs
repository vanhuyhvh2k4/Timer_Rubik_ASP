using AutoMapper;
using Timer_Rubik.WebApp.Authorize.User.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.User.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, UpdateAccountDTO_User>();
            CreateMap<UpdateAccountDTO_User, Account>();
        }
    }
}
