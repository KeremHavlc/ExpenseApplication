﻿using Entity.Concrete;
using Core.DataAccess;
namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
