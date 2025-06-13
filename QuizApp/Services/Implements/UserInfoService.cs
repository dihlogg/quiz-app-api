using AutoMapper;
using QuizApp.Dtos.UserInfo;
using QuizApp.Entities;
using QuizApp.Infrastructures.Repositories;

namespace QuizApp.Services.Implements;

public class UserInfoService : IUserInfoService
{
    private readonly ILogger<UserInfoService> _logger;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IMapper _mapper;

    public UserInfoService(IUserInfoRepository userInfoRepository, ILogger<UserInfoService> logger, IMapper mapper)
    {
        _userInfoRepository = userInfoRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<List<UserInfoDto>> GetUserInfoDtosAsync()
    {
        try
        {
            var data = await _userInfoRepository.GetAllAsync();
            return _mapper.Map<List<UserInfoDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task<bool> AddUserInfoAsync(UserInfoCreateDto userInfoCreateDto)
    {
        try
        {
            var info = _mapper.Map<UserInfo>(userInfoCreateDto);
            return await _userInfoRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditUserInfoAsync(UserInfoDto userInfoDto)
    {
        try
        {
            var userInfo = await _userInfoRepository.GetByIdAsync(userInfoDto.Id);
            if (userInfo == null)
            {
                return null;
            }
            var infoUpdate = _mapper.Map<UserInfo>(userInfoDto);
            var result = await _userInfoRepository.UpdateAsync(infoUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveUserInfoDtosAsync(Guid id)
    {
        try
        {
            return await _userInfoRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
