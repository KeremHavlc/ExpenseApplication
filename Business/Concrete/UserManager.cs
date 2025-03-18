using Business.Abstract;
using Core.Constants;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Entity.Concrete;
using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(UserDto userDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

            Guid defaultRoleId = RoleGuids.User;
            Guid assignedRoleId = userDto.RoleId ?? defaultRoleId;

            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Username = userDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = assignedRoleId
            };

            _userDal.Add(user);
        }

        public void Delete(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email), "Email cannot be null or empty!");
            }

            var user = _userDal.Get(u => u.Email == email);
            if (user is null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            _userDal.Delete(user);
        }

        public List<UserDto> GetAll()
        {
            var users = _userDal.GetAll();
            var listUser = users.Select(user => new UserDto
            {
                Email = user.Email,
                Username = user.Username,
                RoleId = user.RoleId              
            }).ToList();
          
            return listUser;
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public void Update(Guid id ,UserDto userDto)
        {
            Guid defaultRoleId = new Guid("00000000-0000-0000-0000-000000000002");
            Guid assignedRoleId = userDto.RoleId ?? defaultRoleId;

            var existingUser = _userDal.Get(u => u.Id == id); 
            if (existingUser is null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            existingUser.Email = userDto.Email;
            existingUser.Username = userDto.Username;
            existingUser.RoleId = assignedRoleId;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
            }

            _userDal.Update(existingUser);
        }

        
    }
}
