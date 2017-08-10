using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FastBus.Domain.Enums;
using FastBus.Services.Contracts;
using FastBus.Services.Models.User;
using FastBus.Web.Extensions;
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

        public ActionResult Search(UserSearchModel search)
        {
            var result = _userService.Where(Mapper.Map<UserSearchQuery>(search));
            return PartialView("Partials/_Users", Mapper.Map<UserResultViewModel>(result));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(RegisterUserViewModelWithRole model)
        {
            if (ModelState.IsValid)
            {
                var response = await UserManager.Register(model, ModelState);
                if (response.IsSuccessfull)
                {
                    this.FlashSuccess(response.Message);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(string username)
        {
            if (string.IsNullOrEmpty(username)) return RedirectToAction("Index");
            return View(Mapper.Map<EditUserViewModel>(UserManager.FindByName(username)));
        }

        [HttpPost]
        public ActionResult Update(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.Update(Mapper.Map<UserModel>(model));
                this.FlashSuccess("Данные успешно обновленны");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string username)
        {
            return Json(_userService.Delete(username));
        }

        #endregion

    }
}
