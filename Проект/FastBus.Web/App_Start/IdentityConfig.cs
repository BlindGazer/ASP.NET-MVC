using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FastBus.Domain.Entities;
using FastBus.Domain.Entities.Identity;
using FastBus.Domain.Enums;
using FastBus.Persistence;
using FastBus.Web.Extensions;
using FastBus.Web.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace FastBus.Web
{
    public class FastBusRoleStore : RoleStore<Role, int, UserRole>
    {
        public FastBusRoleStore(FastBusDbContext context)
            : base(context)
        {
        }
    }
    public class FastBusRoleManager : RoleManager<Role, int>
    {
        public FastBusRoleManager(FastBusRoleStore roleStore) : base(roleStore)
        {
        }
        public static FastBusRoleManager Create(IdentityFactoryOptions<FastBusRoleManager> options, IOwinContext context)
        {
            return new FastBusRoleManager(new FastBusRoleStore(context.Get<FastBusDbContext>()));
        }
    }

    public class FastBusUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public FastBusUserStore(FastBusDbContext context)
            : base(context)
        {
        }
    }
    public class FastBusUserManager : UserManager<User, int>
    {
        public FastBusUserManager(FastBusUserStore store)
            : base(store)
        {
            UserValidator = new UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            UserLockoutEnabledByDefault = false;
        }
        public static FastBusUserManager Create(IdentityFactoryOptions<FastBusUserManager> options, IOwinContext context)
        {
            return new FastBusUserManager(new FastBusUserStore(context.Get<FastBusDbContext>()));
        }

        public async Task<UserRegisterResponse> Register(RegisterUserViewModelWithRole model, ModelStateDictionary state)
        {
            IdentityResult result;
            switch (model.UserRole)
            {
                case UserRoles.Dispatcher:
                    var dispatcher = Mapper.Map<Dispatcher>(model);
                    result = await CreateAsync(dispatcher, model.Password);

                    if (result.Succeeded)
                    {
                        dispatcher = await FindByNameAsync(dispatcher.UserName) as Dispatcher;
                        if (dispatcher == null) break;

                        await AddToRolesAsync(dispatcher.Id, model.UserRole);
                        return new UserRegisterResponse("Диспетчет успешно добавлен");
                    }
                    break;
                case UserRoles.Driver:
                    var driver = Mapper.Map<Driver>(model);
                    result = await CreateAsync(driver, model.Password);

                    if (result.Succeeded)
                    {
                        driver = await FindByNameAsync(driver.UserName) as Driver;
                        if (driver == null) break;

                        await AddToRolesAsync(driver.Id, model.UserRole);
                        return new UserRegisterResponse("Водитель успешно добавлен");
                    }
                    break;
                default:
                    var user = Mapper.Map<User>(model);
                    result = await CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        user = await FindByNameAsync(user.UserName);

                        await AddToRolesAsync(user.Id, model.UserRole);
                        return new UserRegisterResponse("Пользователь успешно добавлен");
                    }
                    break;
            }
            state.AddIdentityErrors(result);
            return new UserRegisterResponse(false);
        }
    }

    public class FastBusSignInManager : SignInManager<User, int>
    {
        public FastBusSignInManager(FastBusUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static FastBusSignInManager Create(IdentityFactoryOptions<FastBusSignInManager> options, IOwinContext context)
        {
            return new FastBusSignInManager(context.GetUserManager<FastBusUserManager>(), context.Authentication);
        }
    }


}