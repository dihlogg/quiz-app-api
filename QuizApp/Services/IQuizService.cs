using QuizApp.Dtos.Quiz;

namespace QuizApp.Services;

public interface IQuizService
{
    Task<QuizDto> CreateNewQuizAsync();
    Task<QuizDto> RetryQuizAsync(Guid quizId);
}
