using Application.DTOs.Order;
using Application.DTOs.User;
using Application.DTOsNS.UserType;
using Application.Services.UserInformation;
using Application.Services___Repositores.NotficationNS;
using Application.Services___Repositores.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInformationService<UserTypeDTO> _userInformationService;
        private readonly IUserInformationServiceAddress _userAdresses;
        private readonly IOrderService _orderService;

        public UserController(
            IUserInformationService<UserTypeDTO> userInformationService,
            IUserInformationServiceAddress userAdresses,
            IOrderService orderService
        )
        {
            _userInformationService = userInformationService;
            _userAdresses = userAdresses;
            _orderService = orderService;
        }

        [HttpGet("Viewer/{userName}")]
        public async Task<UserTypeDTO> GetViewer([FromRoute] string userName)
        {
            UserInfoRequestDTO userInfoRequest = new UserInfoRequestDTO();
            userInfoRequest.userName = userName;
            userInfoRequest.userType = UserTypeEnum.Viewer.ToString();

            return await _userInformationService.GetUserInformation(userInfoRequest);
        }

        [HttpGet("{id}/Order")]
        public async Task<ActionResult<OrderDtoReturnResultList>> GetAllOrders([FromRoute] int id)
        {
            var response = await _orderService.GetAllOrderByUserID(id);
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        [HttpGet("Address/{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<AddressReturnResultDTO>> GetAddress([FromRoute] int userId)
        {
            var response = await _userAdresses.GetUserAddress(userId);
            if (response == null)
            {
                return NotFound("No Adressses Found For this User");
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }

        [HttpPut("Address/{userId}")]
        [AllowAnonymous]
        public async Task<ActionResult<AddressReturnResultDTO>> UpdateAddress(
            [FromRoute] int userId,
            [FromBody] AddressDTO address
        )
        {
            var response = await _userAdresses.UpdateUserAddress(userId, address);
            if (response == null)
            {
                return NotFound("No Adressses Found For this User");
            }
            var json = JsonConvert.SerializeObject(
                response,
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );
            return Ok(json);
        }
    }
}
