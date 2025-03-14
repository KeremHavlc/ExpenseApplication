using Business.Abstract;
using Entity.Dtos;
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
        public IActionResult AddUser(RegisterDto registerDto)
        {
            _authService.Add(registerDto);
            return Ok("Kullanıcı başarılı bir şekilde kayıt oldu!");
        }
    }
}
