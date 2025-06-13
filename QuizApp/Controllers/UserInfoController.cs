using Microsoft.AspNetCore.Mvc;
using QuizApp.Dtos.UserInfo;
using QuizApp.Services;

namespace QuizApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly ILogger<UserInfoController> _logger;
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(
            ILogger<UserInfoController> logger,
            IUserInfoService userInfoService)
        {
            _logger = logger;
            _userInfoService = userInfoService;
        }

        [HttpGet("GetUserInfos")]
        public async Task<IActionResult> GetUserInfos()
        {
            try
            {
                var data = await _userInfoService.GetUserInfoDtosAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("PostUserInfo")]
        public async Task<IActionResult> PostUserInfo(UserInfoCreateDto userInfoCreateDto)
        {
            try
            {
                var data = await _userInfoService.AddUserInfoAsync(userInfoCreateDto);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutUserInfo")]
        public async Task<IActionResult> PutUserInfo(UserInfoDto userInfoDto)
        {
            try
            {
                var data = await _userInfoService.EditUserInfoAsync(userInfoDto);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUserInfo/{id}")]
        public async Task<IActionResult> DeleteUserInfo(Guid id)
        {
            try
            {
                var data = await _userInfoService.RemoveUserInfoDtosAsync(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
