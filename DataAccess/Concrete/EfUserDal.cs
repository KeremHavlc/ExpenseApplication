using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entity.Concrete;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, SimpleContextDb>, IUserDal
    {
    }
}
