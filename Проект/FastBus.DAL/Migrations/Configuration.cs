using FastBus.DAL.Concrete.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Linq;
using FastBus.DAL.Concrete;
using FastBus.DAL.Enums;


namespace FastBus.DAL.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<FastBusDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FastBusDbContext context)
        {
            #region CreateRoles 
            var roleStore = new RoleStore<Role, int, UserRole>(context);
            var roleManager = new RoleManager<Role, int>(roleStore);
            roleManager.Create(new Role {Name = UserRoles.Admin, Description = UserRoles.AdminDescription });
            roleManager.Create(new Role { Name = UserRoles.Dispatcher, Description = UserRoles.DispatcherDescription});
            roleManager.Create(new Role { Name = UserRoles.Driver, Description = UserRoles.DriverDescription });
            roleManager.Create(new Role { Name = UserRoles.Client, Description = UserRoles.ClientDescription });
            context.SaveChanges();
            #endregion

            #region CreateUsers
            var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context);
            var userManager = new UserManager<User, int>(userStore);

            if (!context.Users.Any(n => n.UserName == UserRoles.Admin))
            {
                var user = new User
                {
                    UserName = UserRoles.Admin,
                    Name = UserRoles.AdminDescription,
                    RegistredDate = System.DateTime.Now
                };
                userManager.Create(user, "Qq!123");
                userManager.AddToRole(user.Id, UserRoles.Admin);
            }
            context.SaveChanges();
            #endregion

            #region CreateData
            //context.SaveChanges();
            #endregion
        }
    }
}
