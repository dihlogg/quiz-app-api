using Microsoft.AspNetCore.Mvc;
using QuizApp.Services;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> _logger;
        private readonly IQuizService _quizService;

        public QuizController(
            ILogger<QuizController> logger,
            IQuizService quizService)
        {
            _logger = logger;
            _quizService = quizService;
        }
        [HttpPost("PostNewQuiz")]
        public async Task<IActionResult> PostNewQuiz()
        {
            try
            {
                var data = await _quizService.CreateNewQuizAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("RetryQuiz/{quizId}")]
        public async Task<IActionResult> RetryQuiz(Guid quizId)
        {
            try
            {
                var data = await _quizService.RetryQuizAsync(quizId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
