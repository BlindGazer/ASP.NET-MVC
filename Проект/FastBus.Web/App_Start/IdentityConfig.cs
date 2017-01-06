using FastBus.DAL.Concrete.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FastBus.DAL.Concrete;
using Microsoft.Owin;

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