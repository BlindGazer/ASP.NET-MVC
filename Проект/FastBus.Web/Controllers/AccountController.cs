using System.Web;
using System.Web.Mvc;
using FastBus.Services.Contracts;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using AutoMapper;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Enums;
using FastBus.Web.Models;
using Microsoft.AspNet.Identity;

namespace FastBus.Web.Controllers
{
    public class AccountController : BaseController
    {
        private FastBusSignInManager _signInManager;
        public FastBusSignInManager SignInManager
        {
            get
            {
                if (_signInManager == null)
                {
                    _signInManager = HttpContext.GetOwinContext().Get<FastBusSignInManager>();
                }
                return _signInManager; ;
            }
        }

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult LogOut(string returnUrl)
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Route");
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //If entered email, than search username
            if (model.UserName.Contains("@"))
            {
                model.UserName = (await UserManager.FindByEmailAsync(model.UserName))?.UserName ?? model.UserName;
            }

            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await UserManager.FindByNameAsync(model.UserName);
                    var isAdmin = await UserManager.IsInRoleAsync(user.Id, UserRoles.Admin);
                    return Redirect(isAdmin && !string.IsNullOrWhiteSpace(returnUrl) ? returnUrl : "/");
                default:
                    ModelState.AddModelError("", "Некорректный логин или пароль");
                    return View(model);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<User>(model);

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user = await UserManager.FindByNameAsync(user.Name);

                    await UserManager.AddToRolesAsync(user.Id, UserRoles.Client);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Route");
                }
                AddErrors(result);
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}