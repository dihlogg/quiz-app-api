using AutoMapper;
using QuizApp.Dtos.Option;
using QuizApp.Dtos.Quiz;
using QuizApp.Infrastructures.Repositories;

namespace QuizApp.Services.Implements;

public class QuizService : IQuizService
{
    private readonly ILogger<QuizService> _logger;
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;

    public QuizService(IQuizRepository quizRepository, ILogger<QuizService> logger, IMapper mapper)
    {
        _quizRepository = quizRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<QuizDto> CreateNewQuizAsync()
    {
        var quiz = await _quizRepository.CreateNewQuizAsync();

        var result = new QuizDto
        {
            Id = quiz.Id,
            StartTime = quiz.StartTime,
            Status = quiz.Status,
            Answers = quiz.Answers.Select(a => new QuizAnswerDto
            {
                QuestionId = a.QuestionId ?? Guid.Empty,
                QuestionText = a.Questions?.Text ?? "",
                Options = a.Questions?.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    IsCorrect = o.IsCorrect,
                    QuestionId = o.QuestionId
                }).ToList() ?? new List<OptionDto>()
            }).ToList()
        };

        return result;
    }

    public async Task<QuizDto> RetryQuizAsync(Guid quizId)
    {
        var quiz = await _quizRepository.RetryQuizAsync(quizId);

        var result = new QuizDto
        {
            Id = quiz.Id,
            StartTime = quiz.StartTime,
            Status = quiz.Status,
            Answers = quiz.Answers.Select(a => new QuizAnswerDto
            {
                QuestionId = a.QuestionId ?? Guid.Empty,
                QuestionText = a.Questions?.Text ?? "",
                Options = a.Questions?.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Text = o.Text,
                    IsCorrect = o.IsCorrect,
                    QuestionId = o.QuestionId
                }).ToList() ?? new List<OptionDto>()
            }).ToList()
        };

        return result;
    }
}
