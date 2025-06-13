using Microsoft.AspNetCore.Mvc;
using QuizApp.Dtos.Question;
using QuizApp.Services;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IQuestionService _questionService;

        public QuestionController(
            ILogger<QuestionController> logger,
            IQuestionService questionService)
        {
            _logger = logger;
            _questionService = questionService;
        }

        [HttpGet("GetQuestionInfos")]
        public async Task<IActionResult> GetQuestionInfos()
        {
            try
            {
                var data = await _questionService.GetQuestionDtosAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("PostQuestion")]
        public async Task<IActionResult> PostQuestion(QuestionCreateDto questionCreateDto)
        {
            try
            {
                var data = await _questionService.AddQuestionAsync(questionCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("PutQuestion")]
        public async Task<IActionResult> PutQuestion(QuestionDto questionDto)
        {
            try
            {
                var data = await _questionService.EditQuestionAsync(questionDto);
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

        [HttpDelete("DeleteQuestion/{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            try
            {
                var data = await _questionService.RemoveQuestionDtosAsync(id);
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
