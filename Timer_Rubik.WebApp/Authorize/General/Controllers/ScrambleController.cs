using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Controllers
{
    [ApiController]
    [Route("api/scramble")]
    public class ScrambleController : Controller
    {

        private readonly IScrambleRepository _scrambleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ScrambleController(IScrambleRepository scrambleRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _scrambleRepository = scrambleRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet("account/{accountId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScrambleOfAccount([FromRoute] Guid accountId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _scrambleRepository
                                   .GetScramblesOfAccount(accountId)
                                   .Select(scramble => new
                                   {
                                       Id = scramble.Id,
                                       Category = new
                                       {
                                           Id = scramble.CategoryId,
                                           Name = scramble.Category.Name
                                       },
                                       Account = new
                                       {
                                           Id = scramble.AccountId,
                                           Name = scramble.Account.Name,
                                           Thumbnail = scramble.Account.Thumbnail,
                                       },
                                       Solve = scramble.Solve?.Answer,
                                       Algorithm = scramble.Algorithm,
                                       Thumbnail = scramble.Thumbnail,
                                       CreatedAt = scramble.CreatedAt,
                                       UpdatedAt = scramble.UpdatedAt,
                                   })
                                   .ToList();

                if (scramble.Count == 0)
                {
                    return NotFound("Not Found Scramble");
                }

                return Ok(scramble);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex,
                });
            }
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScrambleByCategory([FromRoute] Guid categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _scrambleRepository
                                    .GetScrambleByCategory(categoryId)
                                    .Select(scramble => new
                                    {
                                        Id = scramble.Id,
                                        Category = new
                                        {
                                            Id = scramble.CategoryId,
                                            Name = scramble.Category.Name
                                        },
                                        Account = new
                                        {
                                            Id = scramble.AccountId,
                                            Name = scramble.Account.Name,
                                            Thumbnail = scramble.Account.Thumbnail,
                                        },
                                        Solve = scramble.Solve?.Answer,
                                        Algorithm = scramble.Algorithm,
                                        Thumbnail = scramble.Thumbnail,
                                        CreatedAt = scramble.CreatedAt,
                                        UpdatedAt = scramble.UpdatedAt,
                                    })
                                    .ToList();

                if (scramble.Count == 0)
                {
                    return NotFound("Not Found Scramble");
                }

                return Ok(scramble);
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

        [HttpGet("{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _scrambleRepository
                                    .GetScramble(scrambleId);

                if (scramble == null)
                {
                    return NotFound("Not Found Scramble");
                }
                else
                {
                    var scrambleRes = new
                    {
                        Id = scramble.Id,
                        Category = new
                        {
                            Id = scramble.CategoryId,
                            Name = scramble.Category.Name
                        },
                        Account = new
                        {
                            Id = scramble.AccountId,
                            Name = scramble.Account.Name,
                            Thumbnail = scramble.Account.Thumbnail,
                        },
                        Solve = scramble.Solve?.Answer,
                        Algorithm = scramble.Algorithm,
                        Thumbnail = scramble.Thumbnail,
                        CreatedAt = scramble.CreatedAt,
                        UpdatedAt = scramble.UpdatedAt,
                    };
                    return Ok(scrambleRes);
                }
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

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateScrambleDTO createScramble)
        {
            try
            {
                var ownerId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == "UserId")!.Value);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_categoryRepository.CategoryExists(createScramble.CategoryId))
                {
                    return NotFound("Category is not exists");
                }

                var scrambleMap = _mapper.Map<Scramble>(createScramble);

                _scrambleRepository.CreateScramble(ownerId, scrambleMap);

                return Ok("Created successfully");
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
