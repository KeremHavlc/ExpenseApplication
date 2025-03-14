using Business.Abstract;
using Core.Constants;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Add(RegisterDto registerDto)
        {
            if (registerDto.Email == null)
            {
                return ("Email boş olamaz!");
            }        
            if(registerDto.Username == null)
            {
                return ("Kullanıcı adı boş olamaz!");
            }
            if (registerDto.Password == null)
            {
                return ("Şifre boş olamaz!");
            }
            Guid defaultRoleId = RoleGuids.User;
            Guid assignedRoleId = registerDto.RoleId ?? defaultRoleId;
            var userDto = new UserDto
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
                Password = registerDto.Password,
                RoleId = assignedRoleId
            };
            _userService.Add(userDto);
            return ("Kayıt işlemi başarılı!");
        }
    }
}
