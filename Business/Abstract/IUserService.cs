using Entity.Concrete;
using Core.Dto;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(UserDto userDto);
        void Update(Guid id ,UserDto userDto);
        void Delete(string email);
        User GetByEmail(string email);
        List<UserDto> GetAll();
    }
}
