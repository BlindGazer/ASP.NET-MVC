using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace FastBus.Web.Controllers
{
    public class BaseController : Controller
    {
        private FastBusRoleManager _roleManager;
        private FastBusUserManager _userManager;

        public FastBusRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    _roleManager = HttpContext.GetOwinContext().Get<FastBusRoleManager>();
                }
                return _roleManager;
            }
        }
        public FastBusUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = HttpContext.GetOwinContext().GetUserManager<FastBusUserManager>();
                }
                return _userManager;
            }
        }
    }
}