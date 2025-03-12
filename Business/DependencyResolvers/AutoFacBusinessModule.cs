using Autofac;
using Autofac.Core; 
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.Concrete;
using System.Reflection;

namespace Business.DependencyResolvers
{
    public class AutoFacBusinessModule : Autofac.Module // DÜZELTME BURADA
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
        }
    }
}
