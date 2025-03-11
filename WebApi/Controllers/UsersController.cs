using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService<User> _userService;
        public UsersController(IUserService<User> userService)
        {
            _userService = userService;
        }
        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            _userService.Add(user);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
    }
}
