using QuizApp.Entities;

namespace QuizApp.Infrastructures.Repositories;

public interface IQuizRepository : IGenericRepository<QuizInfo>
{
    Task<QuizInfo> CreateNewQuizAsync();
    Task<QuizInfo> RetryQuizAsync(Guid quizId);
}
