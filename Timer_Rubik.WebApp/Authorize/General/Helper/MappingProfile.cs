using AutoMapper;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //category 
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<GetCategoryDTO, Category>();

            //Favorite
            CreateMap<Favorite, GetFavoriteDTO>();
            CreateMap<GetFavoriteDTO, Favorite>();

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
