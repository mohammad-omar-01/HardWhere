using Application;
using Application.DTOs.User;
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
            if (token == LoginCasesEnum.INVALID_USERNAME.ToString())
            {
                return BadRequest(new { Error = "invalid_username" });
            }
            else if (token == LoginCasesEnum.INVALID_PASSWORD.ToString())
            {
                return BadRequest(new { Error = "incorrect_password" });
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

            //var response = _userAuthicticateService.Logout();
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
