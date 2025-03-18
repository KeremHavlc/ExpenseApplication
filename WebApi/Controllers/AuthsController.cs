using Business.Abstract;
using Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService; 
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            _authService.Register(registerDto);
            return Ok("Kullanıcı başarılı bir şekilde kayıt oldu!");
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _authService.Login(loginDto);
            return Ok(result);
        }
    }
}
