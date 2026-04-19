using Application.Features.Authentication.Interfaces;
using Application.Features.Authentication.Login;
using Application.Features.Authentication.Logout;
using Application.Features.Authentication.RefreshTokenFeature;
using Application.Features.Authentication.Register;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {   
        private readonly RegisterHandler _registerHandler;
        private readonly LoginHandler _loginHandler;
        private readonly RefreshTokenHandler _refreshToken;
        private readonly LogoutHandler _logoutHandler;
        public AuthController(RegisterHandler registerHandler, 
                         LoginHandler loginHandler,
                         RefreshTokenHandler refreshTokenHandler,
                         LogoutHandler logoutHandler)
        {
            _registerHandler = registerHandler;
            _loginHandler = loginHandler;
            _refreshToken = refreshTokenHandler;
            _logoutHandler = logoutHandler;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _registerHandler.Handle(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _loginHandler.Handle(command);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand command)
        {
            var result = await _logoutHandler.Handle(command);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _refreshToken.Handle(command);
            return Ok(new {accessToken = result});
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all-users")]
        public IActionResult GetUsers()
        {
            return Ok("Only Admin can see this");
        }
    }
}
