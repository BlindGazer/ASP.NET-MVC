using System.Web.Mvc;
using FastBus.Repositories.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace FastBus.Web
{
	public partial class Startup
	{
	    public void ConfigureAuth(IAppBuilder app)
	    {
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<IUnitOfWork>().Context);

            app.CreatePerOwinContext<FastBusUserManager>(FastBusUserManager.Create);

            app.CreatePerOwinContext<FastBusRoleManager>(FastBusRoleManager.Create);

            app.CreatePerOwinContext<FastBusSignInManager>(FastBusSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}