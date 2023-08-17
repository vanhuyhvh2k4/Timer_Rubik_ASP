using AutoMapper;
using Timer_Rubik.WebApp.DTO.Admin;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Helper.Admin
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, GetAccountDTO>();
            CreateMap<GetAccountDTO, Account>();
        }
    }
}
