using Business.Abstract;
using Core.Constants;
using Core.Utilities.Hashing;
using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Security.Jwt;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        public AuthManager(IUserService userService , ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public Token Login(LoginDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);

            // Kullanıcı kontrolü
            if (user == null)
            {
                return null;
            }

            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);

            if (!result)
            {
                return null;
            }

            var token = _tokenHandler.CreateToken(user.Id, user.Email, user.RoleId.ToString());

            return token;
        }

        public string Register(RegisterDto registerDto)
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
