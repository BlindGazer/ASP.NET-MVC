using System;
using System.Data.Entity;
using FastBus.DAL.Concrete;
using FastBus.Repositories;
using FastBus.Repositories.Contracts;
using FastBus.Services.Contracts;
using FastBus.Services.Services;
using Microsoft.Practices.Unity;

namespace FastBus.Web
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });
        
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
        
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, FastBusDbContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IUserService, UserSerivce>();
            container.RegisterType<ICarService, CarService>();
        }
    }
}
