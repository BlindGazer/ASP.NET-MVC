using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace FastBus.Web.Controllers
{
    public class BaseController : Controller
    {
        private FastBusRoleManager _roleManager;
        private FastBusUserManager _userManager;

        public FastBusRoleManager RoleManager => _roleManager ?? (_roleManager = HttpContext.GetOwinContext().Get<FastBusRoleManager>());
        public FastBusUserManager UserManager => _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<FastBusUserManager>());
    }
}