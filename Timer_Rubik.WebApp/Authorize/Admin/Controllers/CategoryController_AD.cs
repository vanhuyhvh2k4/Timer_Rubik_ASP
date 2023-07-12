using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.Dto;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Authorize.Admin.Repository;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;
using Timer_Rubik.WebApp.Repository;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/category")]
    public class CategoryController_AD : Controller
    {
        private readonly ICategoryRepository_AD _categoryRepository_AD;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController_AD(ICategoryRepository_AD categoryRepository_AD, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository_AD = categoryRepository_AD;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CategoryDto_AD createCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entityCategory = _categoryRepository
                                        .GetCategories()
                                        .Where(cate => cate.Name.Trim().ToUpper() == createCategory.Name.Trim().ToUpper())
                                        .FirstOrDefault();

                if (entityCategory != null)
                {
                    return Conflict("Name Already Exists");
                }

                var categoryMap = _mapper.Map<Category>(createCategory);

                _categoryRepository_AD.CreateCategory(categoryMap);

                return Ok("Created successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory([FromRoute] Guid categoryId, [FromBody] CategoryDto_AD updateCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (categoryId != updateCategory.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_categoryRepository.CategoryExists(categoryId))
                {
                    return NotFound("Not Found Category");
                }

                if (_categoryRepository.GetCategory(updateCategory.Name) != null)
                {
                    return Conflict("Name already exists");
                }

                var categoryMap = _mapper.Map<Category>(updateCategory);

                _categoryRepository_AD.UpdateCategory(categoryMap);

                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }
    }
}
