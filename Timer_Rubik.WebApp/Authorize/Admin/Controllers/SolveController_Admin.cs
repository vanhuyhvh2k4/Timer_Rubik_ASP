using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/solve")]
    public class SolveController_Admin : Controller
    {
        private readonly ISolveRepository_Admin _solveRepository_Admin;
        private readonly IScrambleRepository_Admin _scrambleRepository_Admin;
        private readonly IMapper _mapper;

        public SolveController_Admin(ISolveRepository_Admin solveRepository_Admin, IScrambleRepository_Admin scrambleRepository_Admin, IMapper mapper)
        {
            _solveRepository_Admin = solveRepository_Admin;
            _scrambleRepository_Admin = scrambleRepository_Admin;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSolves()
        {
            try
            {
                var solves = _mapper.Map<List<SolveDto>>(_solveRepository_Admin.GetSolves());

                if (solves.Count == 0)
                {
                    return NotFound("Not Found Solve");
                }

                return Ok(solves);
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

        [HttpGet("{solveId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSolve([FromRoute] Guid solveId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _mapper.Map<SolveDto>(_solveRepository_Admin.GetSolve(solveId));

                if (scramble == null)
                {
                    return NotFound("Not Found Solve");
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

        [HttpGet("scramble/{scrambleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSolveOfScramble([FromRoute] Guid scrambleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var scramble = _mapper.Map<SolveDto>(_solveRepository_Admin.GetSolveOfScramble(scrambleId));

                if (scramble == null)
                {
                    return NotFound("Not Found Solve");
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
        public IActionResult CreateSolve([FromBody] SolveDto createSolve)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_scrambleRepository_Admin.ScrambleExists(createSolve.ScrambleId))
                {
                    return NotFound("Scramble is not exists");
                }

                var solveMap = _mapper.Map<Solve>(createSolve);

                _solveRepository_Admin.CreateSolve(solveMap);

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

        [HttpPut("{solveId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateSolve([FromRoute] Guid solveId, [FromBody] SolveDto updateSolve)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (solveId != updateSolve.Id)
                {
                    return BadRequest("Id is not match");
                }

                var solveMap = _mapper.Map<Solve>(updateSolve);

                _solveRepository_Admin.UpdateSolve(solveMap);

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
    }
}
