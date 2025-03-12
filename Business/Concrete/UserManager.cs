using Business.Abstract;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dtos;
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
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Username = userDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = userDto.RoleId
            };

            _userDal.Add(user);
        }

        public void Delete(UserDto userDto)
        {
            if(userDto is null)
            {
                throw new ArgumentNullException(nameof(userDto),"UserDto cannot be null!");
            }
            var user = _userDal.Get(u => u.Email == userDto.Email);
            if(user is null)
            {
                throw new KeyNotFoundException("User not found!");
            }
            _userDal.Delete(user);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public User GetById(Guid id)
        {
            return _userDal.Get(u => u.Id == id);
        }

        public void Update(UserDto userDto)
        {
           
        }

       
    }
}
