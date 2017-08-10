using System;
using System.Web.Mvc;
using AutoMapper;
using Roles = FastBus.Domain.Enums.UserRoles;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Route;
using FastBus.Web.Extensions;
using FastBus.Web.Models.Route;

namespace FastBus.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        private readonly IScheduleService _scheduleService;
        private readonly IRouteService _routeService;

        public ScheduleController(IScheduleService scheduleService, IRouteService routeService)
        {
            _scheduleService = scheduleService;
            _routeService = routeService;
        }

        #region Public Method

        [HttpGet]
        public ActionResult Index()
        {
            return View(new ScheduleSearchModel(DateTime.Now));
        }

        [HttpPost]
        public ActionResult Search(ScheduleSearchModel search)
        {
            var result = _scheduleService.Where(Mapper.Map<ScheduleSearchQuery>(search));
            return PartialView("Partials/_Schedules", Mapper.Map<ScheduleResultViewModel>(result));
        }

        [HttpGet]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Add()
        {
            return View(new ScheduleAddModel());
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Add(ScheduleAddModel model)
        {
            if (!ModelState.IsValid || !model.InitialDatesIsValid(ModelState))
                return View(model);
            
            if (!User.IsInRole(Roles.Admin))
                model.DispatcherId = User.Identity.GetUserIdInt();

            var response = _scheduleService.Add(Mapper.Map<ScheduleModel>(model), model.DepartureDates, model.DestinationDates);
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Update(long? id)
        {
            if (!id.HasValue || id <= 0) return RedirectToAction("Index");
            var model = Mapper.Map<ScheduleEditModel>(_scheduleService.Get(id.Value));

            if (model == null) return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Update(ScheduleEditModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = _scheduleService.Update(Mapper.Map<ScheduleModel>(model));
            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("", response.Message);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Details(long? id)
        {
            if (!id.HasValue || id <= 0) return null;
            var model = Mapper.Map<ScheduleViewModel>(_scheduleService.Get(id.Value));

            return model == null ? null : PartialView("Partials/_Details", model);
        }
        
        #endregion

        #region Helper Method

        [HttpPost]
        public JsonResult GetDepartures(string departure)
        {
            return Json(_routeService.GetDepartures(departure));
        }

        [HttpPost]
        public JsonResult GetDestinations(string departure, string destination)
        {
            return Json(_routeService.GetDestination(departure, destination));
        }

        #endregion
    }
}
