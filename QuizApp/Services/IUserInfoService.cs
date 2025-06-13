using QuizApp.Dtos.UserInfo;

namespace QuizApp.Services;

public interface IUserInfoService
{
    Task<List<UserInfoDto>> GetUserInfoDtosAsync();
    Task<bool> AddUserInfoAsync(UserInfoCreateDto userInfoCreateDto);
    Task<bool?> EditUserInfoAsync(UserInfoDto userInfoDto);
    Task<bool?> RemoveUserInfoDtosAsync(Guid id);
}
