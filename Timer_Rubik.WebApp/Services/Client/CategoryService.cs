using AutoMapper;
using Timer_Rubik.WebApp.DTO.Client;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services.Client;

namespace Timer_Rubik.WebApp.Services.Client
{
    public class CategoryService : IcategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public APIResponseDTO<ICollection<GetCategoryDTO>> GetCategories()
        {
            var categories = _mapper.Map<List<GetCategoryDTO>>(_categoryRepository.GetCategories());

            if (categories.Count == 0)
            {
                return new APIResponseDTO<ICollection<GetCategoryDTO>>
                {
                    Status = 404,
                    Message = "Not found category"
                };
            }

            return new APIResponseDTO<ICollection<GetCategoryDTO>>
            {
                Status = 200,
                Message = "Successful",
                Data = categories
            };
        }

        public APIResponseDTO<GetCategoryDTO> GetCategory(Guid categoryId)
        {
            var category = _mapper.Map<GetCategoryDTO>(_categoryRepository.GetCategory(categoryId));

            if (category == null)
            {
                return new APIResponseDTO<GetCategoryDTO>
                {
                    Status = 404,
                    Message = "Not found category"
                };
            }

            return new APIResponseDTO<GetCategoryDTO>
            {
                Status = 200,
                Message = "Successful",
                Data = category
            };
        }
    }
}
