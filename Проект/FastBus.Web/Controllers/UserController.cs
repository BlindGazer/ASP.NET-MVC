using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Enums;
using FastBus.DAL.Objects;
using FastBus.Services.Contracts;
using FastBus.Services.Models.User;
using FastBus.Web.Models;
using FastBus.Web.Models.User;
using Microsoft.AspNet.Identity;

namespace FastBus.Web.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        #region publicMethod

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        public ActionResult Index()
        {
            return View(new UserSearchModel());
        }
        
        public ActionResult Get(UserSearchModel search)
        {
            var result = _userService.Where(Mapper.Map<UserSearchQuery>(search));
            return PartialView("Partials/GetUsers", new UserResultViewModel
            {
                Result = Mapper.Map<QueryResult<UserViewModel>>(result),
                Search = search
            });
        }
        
        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(RegisterViewModelWithRole model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<User>(model);
                var result = await UserManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    user = await UserManager.FindByNameAsync(user.UserName);

                    await UserManager.AddToRolesAsync(user.Id, model.UserRole);
                    return RedirectToAction("Index", "User");
                }
                AddErrors(result);
            }
            return View(model);
        }

        #endregion

        #region privateMethod
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion

    }
}
