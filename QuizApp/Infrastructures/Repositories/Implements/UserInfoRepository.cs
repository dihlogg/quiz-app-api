using QuizApp.DataContext;
using QuizApp.Entities;

namespace QuizApp.Infrastructures.Repositories.Implements;

public class UserInfoRepository : GenericRepository<UserInfo>, IUserInfoRepository
{
    public UserInfoRepository(QuizDbContext quizDbContext) : base(quizDbContext)
    {
    }
}
