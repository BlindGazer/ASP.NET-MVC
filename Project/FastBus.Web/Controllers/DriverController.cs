using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using FastBus.Domain.Entities;
using FastBus.Domain.Enums;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Driver;
using FastBus.Web.Extensions;
using FastBus.Web.Helpers;
using FastBus.Web.Models.Driver;
using FastBus.Web.Models.User;
using Microsoft.AspNet.Identity;

namespace FastBus.Web.Controllers
{
    [AuthorizeRoles(UserRoles.Admin, UserRoles.Dispatcher)]
    public class DriverController : BaseController
    {
        private readonly IDriverService _driverService;

        #region publicMethod

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new DriverSearchModel());
        }

        public ActionResult Search(DriverSearchModel search)
        {
            var result = _driverService.Where(Mapper.Map<DriverSearchQuery>(search));

            return PartialView("Partials/_Drivers", Mapper.Map<DriverResultViewModel>(result));
        }

        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Add(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<Driver>(model);
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    user = await UserManager.FindByNameAsync(user.UserName) as Driver;

                    await UserManager.AddToRolesAsync(user.Id, UserRoles.Driver);
                    this.FlashSuccess("Данные успешно добавлены");
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Update(string username, int? id)
        {
            Driver driver = null;

            if (!string.IsNullOrEmpty(username))
            {
                driver = UserManager.FindByName(username) as Driver;
            }
            else if (id.HasValue)
            {

                driver = UserManager.FindById(id.Value) as Driver;
            }
            if(driver == null)
            {
                return RedirectToAction("Index");
            }

            return View(Mapper.Map<EditDriverViewModel>(driver));
        }
        
        [HttpPost]
        public ActionResult Update(EditDriverViewModel model)
        {
            if (ModelState.IsValid)
            {
                _driverService.Update(Mapper.Map<DriverModel>(model));
                this.FlashSuccess("Данные успешно обновлены");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string username)
        {
            return Json(_driverService.Delete(username));
        }

        #endregion

        #region Helper

        [HttpPost]
        public JsonResult Drivers(int carId)
        {
            return Json(SelectListHelper.GetDrivers(carId));
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
