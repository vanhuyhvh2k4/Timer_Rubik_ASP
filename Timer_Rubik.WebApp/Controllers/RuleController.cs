using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Interfaces;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Controllers
{
    [ApiController]
    [Route("api/rule")]
    public class RuleController : Controller
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly IMapper _mapper;

        public RuleController(IRuleRepository ruleRepository, IMapper mapper)
        {
            _ruleRepository = ruleRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRules()
        {
            try
            {
                var rules = _mapper.Map<List<RuleDto>>(_ruleRepository.GetRules());

                if (rules.Count == 0)
                {
                    return NotFound("Not Found Rule");
                }

                return Ok(rules);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong",
                    Message = ex.Message,
                });
            }
        }


        [HttpGet("{ruleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRule([FromRoute] Guid ruleId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rule = _mapper.Map<RuleDto>(_ruleRepository.GetRule(ruleId));

                if (rule == null)
                {
                    return NotFound("Not Found Rule");
                }

                return Ok(rule);
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
