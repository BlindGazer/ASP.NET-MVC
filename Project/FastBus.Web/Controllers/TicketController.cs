using System;
using System.Web.Mvc;
using AutoMapper;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Route;
using FastBus.Web.Extensions;
using FastBus.Web.Models.Route;

namespace FastBus.Web.Controllers
{
    public class TicketController : BaseController
    {
        private readonly ITicketsService _ticketsService;

        public TicketController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }
        #region publicMethod

        [HttpGet]
        public ActionResult Index()
        {
            return View(new TicketSearchModel { DepartureDate = DateTime.Now, DestinationDate = DateTime.Now.AddDays(7) });
        }

        [HttpPost]
        public ActionResult Search(TicketSearchModel model)
        {
            var search = Mapper.Map<TicketSearchQuery>(model);
            search.BuyerId = User.Identity.GetUserIdInt();
            return PartialView("Partials/_Tickets", Mapper.Map<TicketResultViewModel>(_ticketsService.Where(search)));
        }

        #endregion

    }
}
