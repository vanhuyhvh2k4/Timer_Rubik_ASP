using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/scramble")]
    public class ScrambleController_Admin : Controller
    {
        private readonly IScrambleService_Admin _scrambleRepository_Admin;
        private readonly IAccountService_Admin _accountRepository_Admin;
        private readonly ICategoryService_Admin _categoryRepository_Admin;
        private readonly IMapper _mapper;

        public ScrambleController_Admin(IScrambleService_Admin scrambleRepository_Admin, IAccountService_Admin accountRepository_Admin, ICategoryService_Admin categoryRepository_Admin, IMapper mapper)
        {
            _scrambleRepository_Admin = scrambleRepository_Admin;
            _accountRepository_Admin = accountRepository_Admin;
            _categoryRepository_Admin = categoryRepository_Admin;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetScrambles()
        {
            try
            {
                var scrambles = _scrambleRepository_Admin
                                    .GetScrambles()
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
                                            Email = scramble.Account.Email
                                        },
                                        Solve = scramble.Solve.Answer,
                                        Algorithm = scramble.Algorithm,
                                        Thumbnail = scramble.Thumbnail,
                                        CreatedAt = scramble.CreatedAt,
                                        UpdatedAt = scramble.UpdatedAt,
                                    })
                                    .ToList();

                if (scrambles.Count == 0)
                {
                    return NotFound("Not Found Scramble");
                }

                return Ok(scrambles);
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

                var scramble = _scrambleRepository_Admin
                                    .GetScramble(scrambleId);

                if (scramble == null)
                {
                    return NotFound("Not Found Scramble");
                } else
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
                            Email = scramble.Account.Email
                        },
                        Solve = scramble.Solve.Answer,
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

                var scramble = _scrambleRepository_Admin
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
                                           Email = scramble.Account.Email
                                       },
                                       Solve = scramble.Solve.Answer,
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

                var scramble = _scrambleRepository_Admin
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
                                            Email = scramble.Account.Email
                                        },
                                        Solve = scramble.Solve.Answer,
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CreateScrambleDTO_Admin createScramble)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_accountRepository_Admin.AccountExists(createScramble.AccountId))
                {
                    return NotFound("Account is not exists");
                }

                if (!_categoryRepository_Admin.CategoryExists(createScramble.CategoryId))
                {
                    return NotFound("Category is not exists");
                }

                var scrambleMap = _mapper.Map<Scramble>(createScramble);

                _scrambleRepository_Admin.CreateScramble(scrambleMap);

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

        [HttpPut("{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateScramble([FromRoute] Guid scrambleId, [FromBody] UpdateScrambleDTO_Admin updateScramble)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (scrambleId != updateScramble.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_categoryRepository_Admin.CategoryExists(updateScramble.CategoryId))
                {
                    return NotFound("Not Found Category");
                }
                
                if (!_accountRepository_Admin.AccountExists(updateScramble.AccountId))
                {
                    return NotFound("Not Found Account");
                }

                var categoryMap = _mapper.Map<Scramble>(updateScramble);

                _scrambleRepository_Admin.UpdateScramble(categoryMap);

                return Ok("Updated successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete("{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_scrambleRepository_Admin.ScrambleExists(scrambleId))
                {
                    return NotFound("Not Found Scramble");
                }

                var scrambleEntity = _scrambleRepository_Admin.GetScramble(scrambleId);

                _scrambleRepository_Admin.DeleteScramble(scrambleEntity);

                return Ok("Deleted successfully");
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
