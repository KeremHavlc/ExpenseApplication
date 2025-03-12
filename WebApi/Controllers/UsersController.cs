using Business.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(UserDto userDto)
        {
            _userService.Add(userDto);
            return Ok();
        }


        [HttpDelete("DeleteUserByEmail/{email}")]
        public IActionResult DeleteUser(string email)
        {
            _userService.Delete(email);
            return Ok();
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }


        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser([FromRoute(Name = "id")] Guid id, [FromBody] UserDto userDto)
        {
            _userService.Update(id,userDto);
            return Ok("User updated successfully!");                      
        }
    }
}
