using QuizApp.DataContext;
using QuizApp.Entities;

namespace QuizApp.Infrastructures.Repositories.Implements;

public class OptionRepository : GenericRepository<Option>, IOptionRepository
{
    public OptionRepository(QuizDbContext quizDbContext) : base(quizDbContext)
    {
    }
}
