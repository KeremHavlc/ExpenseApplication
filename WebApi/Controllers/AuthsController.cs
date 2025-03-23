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

            if (result == null || string.IsNullOrEmpty(result.AccessToken))
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");
            }

            // Cookie'yi ekleme işlemi
            Response.Cookies.Append("authToken", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,      // XSS saldırılarına karşı koruma sağlar
                Secure = false,        // HTTPS üzerinde çalışır (dev ortamında HTTPS kullanmazsan `false` yapabilirsin)
                SameSite = SameSiteMode.Strict, // CSRF saldırılarına karşı koruma sağlar
                Expires = DateTimeOffset.UtcNow.AddHours(1) // Token süresi kadar ayarlayabilirsin
            });

            return Ok(new { message = "Giriş başarılı." });
        }
    }
}
