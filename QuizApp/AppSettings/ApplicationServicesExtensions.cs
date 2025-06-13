using QuizApp.Infrastructures;
using QuizApp.Infrastructures.Repositories;
using QuizApp.Infrastructures.Repositories.Implements;
using QuizApp.Services;
using QuizApp.Services.Implements;

namespace QuizApp.AppSettings
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServicesExtension(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserInfoRepository, UserInfoRepository>();
            services.AddTransient<IUserInfoService, UserInfoService>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IOptionRepository, OptionRepository>();
            services.AddTransient<IOptionService, OptionService>();
            services.AddTransient<IQuizRepository, QuizRepository>();
            services.AddTransient<IQuizService, QuizService>();
        }
    }
}
