using Application.DTOsNS;
using Application.DTOsNS.UserType;
using Application.Services.UserInformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInformationService<UserTypeDTO> _userInformationService;

        public UserController(IUserInformationService<UserTypeDTO> userInformationService)
        {
            _userInformationService = userInformationService;
        }

        [HttpGet("Viewer/{userName}")]
        public async Task<UserTypeDTO> GetViewer([FromRoute] string userName)
        {
            UserInfoRequestDTO userInfoRequest = new UserInfoRequestDTO();
            userInfoRequest.userName = userName;
            userInfoRequest.userType = UserTypeEnum.Viewer.ToString();

            return await _userInformationService.GetUserInformation(userInfoRequest);
        }
    }
}
