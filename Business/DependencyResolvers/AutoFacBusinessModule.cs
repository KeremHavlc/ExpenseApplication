using Autofac;
using Autofac.Core; 
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Business.DependencyResolvers
{
    public class AutoFacBusinessModule : Autofac.Module // DÜZELTME BURADA
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

            builder.RegisterType<ExpenseManager>().As<IExpenseService>();
            builder.RegisterType<EfExpenseDal>().As<IExpenseDal>();

            builder.RegisterType<CurrentUserService>().As<ICurrentUserService>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
        }
    }
}
