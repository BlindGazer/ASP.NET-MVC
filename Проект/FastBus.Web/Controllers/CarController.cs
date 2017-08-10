using System.Web.Mvc;
using AutoMapper;
using FastBus.Domain.Enums;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Car;
using FastBus.Web.Extensions;
using FastBus.Web.Models.Car;

namespace FastBus.Web.Controllers
{
    [AuthorizeRoles(UserRoles.Admin, UserRoles.Dispatcher)]
    public class CarController : BaseController
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new CarSearchModel());
        }

        [HttpPost]
        public ActionResult Search(CarSearchModel search)
        {
            var result = _carService.Where(Mapper.Map<CarSearchQuery>(search));
            return PartialView("Partials/_Cars", Mapper.Map<CarResultViewModel>(result));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddCarViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            
            _carService.Add(Mapper.Map<CarModelWithDrivers>(model));
            this.FlashSuccess("Данные успешно добавлены");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if(id == null || id <= 0)
                return RedirectToAction("Index");

            var model = Mapper.Map<AddCarViewModel>(_carService.Get(id.Value));

            if(model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        
        [HttpPost]
        public ActionResult Update(AddCarViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _carService.Update(Mapper.Map<CarModelWithDrivers>(model));
            this.FlashSuccess("Данные успешно обновленны");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return Json(_carService.Delete(id));
        }
    }
}
