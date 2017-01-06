using System.Web.Mvc;
using AutoMapper;
using FastBus.DAL.Enums;
using FastBus.DAL.Objects;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Car;
using FastBus.Web.Attributes;
using FastBus.Web.Models.Car;

namespace FastBus.Web.Controllers
{
    [AuthorizeRoles(UserRoles.Admin, UserRoles.Dispatcher)]
    public class CarController : BaseController
    {
        private readonly ICarService _carService;
        #region publicMethod

        public CarController(ICarService carService, IUserService userService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new CarSearchModel());
        }

        [HttpPost]
        public ActionResult Get(CarSearchModel search)
        {
            var result = _carService.Where(Mapper.Map<CarSearchQuery>(search));
            return PartialView("Partials/GetCars", new CarResultViewModel
            {
                Result = Mapper.Map<QueryResult<CarViewModel>>(result),
                Search = search
            });
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Update");
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            if(id == null || id <= 0) return RedirectToAction("Index");
            var model = Mapper.Map<AddCarViewModel>(_carService.Get(id.Value));

            if(model == null) return RedirectToAction("Index");
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Update(AddCarViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id.HasValue)
            {
                _carService.Update(Mapper.Map<CarModel>(model));
            }
            else
            {
                _carService.Add(Mapper.Map<CarModel>(model));
            }
            return RedirectToAction("Index");
        }

        #endregion

    }
}
