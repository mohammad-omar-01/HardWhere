using Application.DTOs;
using Application.Services.Authintication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

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
            var loginResult = _userAuthicticateService.Login(loginRequest);
            if (loginResult.Token == null)
            {
                return BadRequest();
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
            };

            Response.Cookies.Append("Authorization", loginResult.Token, cookieOptions);

            var reponse = new { Status = "Success", AccountDeatils = loginResult.AccountDetails };
            var json = JsonConvert.SerializeObject(reponse);
            return Content(json, "application/json", Encoding.UTF8);
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

        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout([FromBody] LogoutRequestDTO logoutRequest)
        {
            var response = _userAuthicticateService.Logout(logoutRequest);
            if (response == false)
            {
                return BadRequest("User Not LoggedIn");
            }
            return Ok("Logged Out Successfully");
        }
    }
}
