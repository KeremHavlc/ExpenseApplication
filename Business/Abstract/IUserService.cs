using Entity.Concrete;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IUserService<T> where T : User
    {
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        T GetById(Guid id);
        List<User> GetAll();
    }
}
