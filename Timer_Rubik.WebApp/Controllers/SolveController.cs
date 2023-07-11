using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Repository;

namespace Timer_Rubik.WebApp.Controllers
{
    [ApiController]
    [Route("api/solve")]
    public class SolveController : Controller
    {
        private readonly ISolveRepository _solveRepository;
        private readonly IMapper _mapper;

        public SolveController(ISolveRepository solveRepository, IMapper mapper)
        {
            _solveRepository = solveRepository;
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
                var solves = _mapper.Map<List<SolveDto>>(_solveRepository.GetSolves());

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

                var scramble = _mapper.Map<SolveDto>(_solveRepository.GetSolve(solveId));

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

                var scramble = _mapper.Map<SolveDto>(_solveRepository.GetSolveOfScramble(scrambleId));

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
    }
}
