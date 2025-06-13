using QuizApp.Dtos.Option;

namespace QuizApp.Services;

public interface IOptionService
{
    Task<List<OptionDto>> GetOptionDtosAsync();
    Task<bool> AddOptionAsync(OptionCreateDto optionCreateDto);
    Task<bool?> EditOptionAsync(OptionDto optionDto);
    Task<bool?> RemoveOptionDtosAsync(Guid id);
}