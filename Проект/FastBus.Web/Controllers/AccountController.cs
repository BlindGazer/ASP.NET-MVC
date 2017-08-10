using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using AutoMapper;
using FastBus.Domain.Entities;
using FastBus.Domain.Enums;
using FastBus.Services.Contracts;
using FastBus.Services.Models;
using FastBus.Services.Models.Route;
using FastBus.Web.Extensions;
using FastBus.Web.Models.Route;
using FastBus.Web.Models.User;
using Microsoft.AspNet.Identity;

namespace FastBus.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ITicketsService _ticketsService;
        private FastBusSignInManager _signInManager;

        public FastBusSignInManager SignInManager => _signInManager ?? 
            (_signInManager = HttpContext.GetOwinContext().Get<FastBusSignInManager>());

        public AccountController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult BuyerInfo()
        {
            var buyer = UserManager.FindByName(User.Identity.Name) as Buyer;
            return PartialView("Partials/_BuyerInfo", buyer);
        }

        public ActionResult GetUpcomingTicket()
        {
            var search = new TicketSearchQuery(int.Parse(User.Identity.GetUserId()));
            return PartialView("Partials/_Tickets", Mapper.Map<TicketResultViewModel>(_ticketsService.Where(search)));
        }

        [HttpGet]
        public ActionResult LogOut(string returnUrl)
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Schedule");
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
                    ModelState.AddModelError("", @"Некорректный логин или пароль");
                    return View(model);
            }
        }
        
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Schedule");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var buyer = Mapper.Map<Buyer>(model);

                var result = await UserManager.CreateAsync(buyer, model.Password);
                if (result.Succeeded)
                {
                    buyer = await UserManager.FindByNameAsync(buyer.UserName) as Buyer;

                    await UserManager.AddToRolesAsync(buyer.Id, UserRoles.Buyer);
                    await SignInManager.SignInAsync(buyer, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Schedule");
                }
                ModelState.AddIdentityErrors(result);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Json(new ServiceResponse(true, User.Identity.GetUserId()));
                }
                var buyer = Mapper.Map<Buyer>(model);

                var result = await UserManager.CreateAsync(buyer, model.Password);
                if (result.Succeeded)
                {
                    buyer = await UserManager.FindByNameAsync(buyer.UserName) as Buyer;

                    await UserManager.AddToRolesAsync(buyer.Id, UserRoles.Buyer);
                    await SignInManager.SignInAsync(buyer, isPersistent: false, rememberBrowser: false);

                    return Json(new ServiceResponse(true, buyer.Id.ToString()));
                }
                ModelState.AddIdentityErrors(result);
            }
            return Json(new ServiceResponse(false, ModelState.GetErrors()));
        }

        [HttpPost]
        public ActionResult GetAppBar()
        {
            return PartialView("Partials/_AppBar");
        }

        [HttpPost]
        public async Task<JsonResult> EditPassword(EditPasswordModel model)
        {
            if(!ModelState.IsValid) return Json(new ServiceResponse(false, ModelState.GetErrors("\n")));
            var id = User.Identity.GetUserIdInt();
            var result = await UserManager.ChangePasswordAsync(id, model.CurrentPassword, model.NewPassword);
            return Json(result.Succeeded
                ? new ServiceResponse(true, "Пароль успешно изменен")
                : new ServiceResponse(false, "Неверный пароль"));
        }
    }
}