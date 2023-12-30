using Application.DTOsNS;
using Application.Services.Authintication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardWherePresenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authuntication : ControllerBase
    {
        private readonly IUserAuthicticateService _userAuthicticateService;

        public Authuntication(IUserAuthicticateService userAuthicticateService)
        {
            _userAuthicticateService = userAuthicticateService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserSignInDTO loginRequest)
        {
            var token = _userAuthicticateService.Login(loginRequest);
            if (token == null)
            {
                return BadRequest();
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTime.UtcNow.AddHours(6)
            };

            Response.Cookies.Append("Authorization", token, cookieOptions);

            return Ok(new { Status = "Success", token });
        }

        [HttpPost("Signup")]
        public IActionResult SignUp([FromBody] UserSignUpDTO userSignUpRequest)
        {
            var response = _userAuthicticateService.SignUp(userSignUpRequest);
            if (response == false)
            {
                return BadRequest("User Already Exists");
            }
            return Ok();
        }

        [HttpGet("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"];

            //var response = _userAuthicticateService.Logout(logoutRequest);
            //if (response == false)
            //{
            //    return BadRequest("User Not LoggedIn");
            //}
            var cookieOptions = new CookieOptions { HttpOnly = true, Expires = DateTime.UtcNow };

            Response.Cookies.Append("Authorization", "", cookieOptions);
            return Ok("Logged Out Successfully");
        }
    }
}
