using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Attributes;
using Timer_Rubik.WebApp.Authorize.Admin.DTO;
using Timer_Rubik.WebApp.Interfaces;

namespace Timer_Rubik.WebApp.Authorize.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/solve")]
    public class SolveController_Admin : Controller
    {
        private readonly ISolveRepository _solveRepository;
        private readonly IMapper _mapper;

        public SolveController_Admin(ISolveRepository solveRepository, IMapper mapper)
        {
            _solveRepository = solveRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AdminToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetSolves()
        {
            try
            {
                var solves = _mapper.Map<List<GetSolveDTO_Admin>>(_solveRepository.GetSolves());

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
    }
}
