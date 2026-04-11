using Application.Features.Authentication.Interfaces;
using Application.Features.Authentication.Login;
using Application.Features.Authentication.RefreshToken;
using Application.Features.Authentication.Register;
using Domain.Interfaces;
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
        public AuthController(IUserRepository userRepository, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository)
        {
            _registerHandler = new RegisterHandler(userRepository);
            _loginHandler = new LoginHandler(userRepository, tokenService, refreshTokenRepository);    
            _refreshToken = new RefreshTokenHandler(refreshTokenRepository, userRepository, tokenService);    
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

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _refreshToken.Handle(command);
            return Ok(new {accessToken = result});
        }
    }
}
