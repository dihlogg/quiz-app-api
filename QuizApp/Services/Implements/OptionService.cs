using AutoMapper;
using QuizApp.Dtos.Option;
using QuizApp.Entities;
using QuizApp.Infrastructures.Repositories;

namespace QuizApp.Services.Implements;

public class OptionService : IOptionService
{
    private readonly ILogger<OptionService> _logger;
    private readonly IOptionRepository _optionRepository;
    private readonly IMapper _mapper;

    public OptionService(IOptionRepository optionRepository, ILogger<OptionService> logger, IMapper mapper)
    {
        _optionRepository = optionRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<bool> AddOptionAsync(OptionCreateDto optionCreateDto)
    {
        try
        {
            var info = _mapper.Map<Option>(optionCreateDto);
            return await _optionRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditOptionAsync(OptionDto optionDto)
    {
        try
        {
            var option = await _optionRepository.GetByIdAsync(optionDto.Id);
            if (option == null)
            {
                return null;
            }
            var infoUpdate = _mapper.Map<Option>(optionDto);
            var result = await _optionRepository.UpdateAsync(infoUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<List<OptionDto>> GetOptionDtosAsync()
    {
        try
        {
            var data = await _optionRepository.GetAllAsync();
            return _mapper.Map<List<OptionDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveOptionDtosAsync(Guid id)
    {
        try
        {
            return await _optionRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
