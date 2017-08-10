using System.Web.Mvc;
using AutoMapper;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Route;
using FastBus.Web.Extensions;
using FastBus.Web.Models.Route;
using Roles = FastBus.Domain.Enums.UserRoles;

namespace FastBus.Web.Controllers
{
    public class RouteController : BaseController
    {
        private readonly IRouteService _routeService;
        
        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new RouteSearchModel());
        }

        [HttpPost]
        public ActionResult Search(RouteSearchModel search)
        {
            var result = _routeService.Where(Mapper.Map<RouteSearchQuery>(search));
            return PartialView("Partials/_Routes", Mapper.Map<RouteResultViewModel>(result));
        }

        [HttpGet]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Add(RouteViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _routeService.Add(Mapper.Map<RouteModel>(model));
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Update(int? id)
        {
            if (!id.HasValue || id <= 0) return RedirectToAction("Index");
            var model = Mapper.Map<RouteViewModel>(_routeService.Get(id.Value));

            if (model == null) return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Update(RouteViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _routeService.Update(Mapper.Map<RouteModel>(model));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [AuthorizeRoles(Roles.Admin, Roles.Dispatcher)]
        public ActionResult Delete(int id)
        {
            return Json(_routeService.Delete(id));
        }

    }
}
