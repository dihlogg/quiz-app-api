using Microsoft.AspNetCore.Mvc;
using QuizApp.Dtos.Option;
using QuizApp.Services;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionController : ControllerBase
    {
        private readonly ILogger<OptionController> _logger;
        private readonly IOptionService _optionService;

        public OptionController(
            ILogger<OptionController> logger,
            IOptionService optionService)
        {
            _logger = logger;
            _optionService = optionService;
        }

        [HttpGet("GetOptionInfos")]
        public async Task<IActionResult> GetOptionInfos()
        {
            try
            {
                var data = await _optionService.GetOptionDtosAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("PostOption")]
        public async Task<IActionResult> PostOption(OptionCreateDto optionCreateDto)
        {
            try
            {
                var data = await _optionService.AddOptionAsync(optionCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutOption")]
        public async Task<IActionResult> PutOption(OptionDto optionDto)
        {
            try
            {
                var data = await _optionService.EditOptionAsync(optionDto);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteOption/{id}")]
        public async Task<IActionResult> DeleteOption(Guid id)
        {
            try
            {
                var data = await _optionService.RemoveOptionDtosAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
