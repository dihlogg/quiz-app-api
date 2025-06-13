using Microsoft.EntityFrameworkCore;
using QuizApp.DataContext;
using QuizApp.Entities;

namespace QuizApp.Infrastructures.Repositories.Implements;

public class QuizRepository : GenericRepository<QuizInfo>, IQuizRepository
{
    public QuizRepository(QuizDbContext quizDbContext) : base(quizDbContext)
    {
    }

    public async Task<QuizInfo> CreateNewQuizAsync()
    {
        var questions = await _quizDbContext.Questions
        .Include(q => q.Options)
        .OrderBy(q => Guid.NewGuid())
        .Take(10)
        .ToListAsync();

        var quiz = new QuizInfo
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.UtcNow,
            Status = "Doing",
            QuizStatus = null
        };

        quiz.Answers = questions.Select(q => new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = q.Id,
            QuizInfoId = quiz.Id
        }).ToList();

        _quizDbContext.QuizInfos.Add(quiz);
        await _quizDbContext.SaveChangesAsync();

        // Attach loaded questions to answers for mapping DTO
        foreach (var answer in quiz.Answers)
        {
            answer.Questions = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
        }

        return quiz;
    }

    public async Task<QuizInfo> RetryQuizAsync(Guid quizId)
    {
        var previousAnswers = await _quizDbContext.Answers
            .Where(a => a.QuizInfoId == quizId)
            .Select(a => a.QuestionId)
            .ToListAsync();

        if (!previousAnswers.Any())
        {
            throw new Exception("No questions found for the specified quiz.");
        }

        var questions = await _quizDbContext.Questions
            .Include(q => q.Options)
            .Where(q => previousAnswers.Contains(q.Id))
            .ToListAsync();

        var quiz = new QuizInfo
        {
            Id = Guid.NewGuid(),
            StartTime = DateTime.UtcNow,
            Status = "Doing",
            QuizStatus = null
        };

        quiz.Answers = questions.Select(q => new Answer
        {
            Id = Guid.NewGuid(),
            QuestionId = q.Id,
            QuizInfoId = quiz.Id
        }).ToList();


        _quizDbContext.QuizInfos.Add(quiz);
        await _quizDbContext.SaveChangesAsync();

        // Attach loaded questions to answers for mapping DTO
        foreach (var answer in quiz.Answers)
        {
            answer.Questions = questions.FirstOrDefault(q => q.Id == answer.QuestionId);
        }

        return quiz;
    }
}
