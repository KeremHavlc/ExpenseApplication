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

        public (bool success, string message) Register(RegisterDto registerDto)
        {
            //IsNullOrWhiteSpace null , "" veya boşluk karakteri mi içerdiğini kontrol eder.Bunlardan biri ise true döner.
            //yani true dönmesi boş dönmesi demektir ve return de false döndeririz.
            if (string.IsNullOrWhiteSpace(registerDto.Email))
                return (false, "Email boş olamaz!");

            if (string.IsNullOrWhiteSpace(registerDto.Username))
                return (false, "Kullanıcı adı boş olamaz!");

            if (string.IsNullOrWhiteSpace(registerDto.Password))
                return (false, "Şifre boş olamaz!");
            Guid defaultRoleId = RoleGuids.User;
            Guid assignedRoleId = registerDto.RoleId ?? defaultRoleId;
            var userDto = new UserDto
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
                Password = registerDto.Password,
                RoleId = assignedRoleId
            };
            try
            {
                _userService.Add(userDto);
                // Kullanıcı oluşturma işlemleri
                return (true, "Kayıt başarılı!");
            }
            catch
            {
                return (false, "Kayıt sırasında hata oluştu");
            }
        }
    }
}
