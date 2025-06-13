using QuizApp.DataContext;
using QuizApp.Entities;

namespace QuizApp.Infrastructures.Repositories.Implements;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
    public QuestionRepository(QuizDbContext quizDbContext) : base(quizDbContext)
    {
    }
}
