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
            var (success, message) = _authService.Register(registerDto);
            if(success)
                return Ok(new { success = true , message});

            return BadRequest(new { success = false, message });
        }


        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _authService.Login(loginDto);

            if (result == null || string.IsNullOrEmpty(result.AccessToken))
            {
                return Unauthorized(new { message = "Kullanici Adi veya Şifre Hatali !" });
            }

            // Cookie'yi ekleme işlemi
            Response.Cookies.Append("authToken", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // HTTPS varsa true, HTTP’de false
                SameSite = SameSiteMode.None
            });
            
            return Ok(new { message = "Giriş başarılı." });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (Request.Cookies["AuthToken"] != null)
            {

                Response.Cookies.Append("authToken", "",new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
                
            }

            return Ok(new { message = "Logout successful." });
        }
    }
}
