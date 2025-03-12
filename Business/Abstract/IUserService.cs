using Entity.Concrete;
using Entity.Dtos;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(UserDto userDto);
        void Update(UserDto userDto);
        void Delete(UserDto userDto);
        User GetById(Guid id);
        List<User> GetAll();
    }
}
