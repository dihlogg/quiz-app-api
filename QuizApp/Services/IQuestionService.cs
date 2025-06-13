using QuizApp.Dtos.Question;

namespace QuizApp.Services;

public interface IQuestionService
{
    Task<List<QuestionDto>> GetQuestionDtosAsync();
    Task<bool> AddQuestionAsync(QuestionCreateDto questionCreateDto);
    Task<bool?> EditQuestionAsync(QuestionDto questionDto);
    Task<bool?> RemoveQuestionDtosAsync(Guid id);
}
