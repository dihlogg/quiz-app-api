using AutoMapper;
using QuizApp.Dtos.Question;
using QuizApp.Entities;
using QuizApp.Infrastructures.Repositories;

namespace QuizApp.Services.Implements;

public class QuestionService : IQuestionService
{
    private readonly ILogger<QuestionService> _logger;
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionService(IQuestionRepository questionRepository, ILogger<QuestionService> logger, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<List<QuestionDto>> GetQuestionDtosAsync()
    {
        try
        {
            var data = await _questionRepository.GetAllAsync();
            return _mapper.Map<List<QuestionDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task<bool> AddQuestionAsync(QuestionCreateDto questionCreateDto)
    {
        try
        {
            var info = _mapper.Map<Question>(questionCreateDto);
            return await _questionRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task<bool?> EditQuestionAsync(QuestionDto questionDto)
    {
        try
        {
            var question = await _questionRepository.GetByIdAsync(questionDto.Id);
            if (question == null)
            {
                return null;
            }
            var infoUpdate = _mapper.Map<Question>(questionDto);
            var result = await _questionRepository.UpdateAsync(infoUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveQuestionDtosAsync(Guid id)
    {
        try
        {
            return await _questionRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
