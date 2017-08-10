using System;
using System.Data.Entity;
using System.Linq;
using AutoMapper.QueryableExtensions;
using FastBus.DAL.Contracts;
using FastBus.Domain.Entities;
using FastBus.Services.Contracts;
using FastBus.Domain.Objects;
using FastBus.Services.Models.Route;

namespace FastBus.Services.Services
{
    public class TicketService : BaseService, ITicketsService
    {
        public TicketService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public QueryResult<TicketModelWithSchedule> Where(TicketSearchQuery searchQuery)
        {
            var result = new QueryResult<TicketModelWithSchedule>();
            var ticketRepo = _uow.GetRepostirory<Ticket>();
            result.Total = ticketRepo.All.Count(x => x.BuyerId == searchQuery.BuyerId);

            bool hasDestination = !string.IsNullOrWhiteSpace(searchQuery.Destination),
                hasDeparture = !string.IsNullOrWhiteSpace(searchQuery.Departure),
                hasWaypoint = !string.IsNullOrWhiteSpace(searchQuery.WayPoint);
            var currentDate = DateTime.Now.Date;

            var query = ticketRepo.Where( x => !searchQuery.BuyerId.HasValue || x.BuyerId == searchQuery.BuyerId,
                x => !searchQuery.IsPayment.HasValue || x.IsPaid == searchQuery.IsPayment,
                x => !hasDestination || x.ScheduleItem.Route.Destination.Equals(searchQuery.Destination, StringComparison.CurrentCultureIgnoreCase),
                x => !hasDeparture || x.ScheduleItem.Route.Departure.Equals(searchQuery.Departure, StringComparison.CurrentCultureIgnoreCase),
                x => !searchQuery.DepartureDate.HasValue && DbFunctions.TruncateTime(x.ScheduleItem.DepartureDate) >= currentDate
                     || DbFunctions.TruncateTime(x.ScheduleItem.DepartureDate) >= searchQuery.DepartureDate,
                x => !searchQuery.DestinationDate.HasValue || DbFunctions.TruncateTime(x.ScheduleItem.DestinationDate) <= searchQuery.DestinationDate,
                x => !hasDeparture || x.ScheduleItem.Route.Departure.ToLower().Contains(searchQuery.Departure.ToLower()),
                x => !hasWaypoint ||
                     x.ScheduleItem.Route.WayPoints.Any(wp => wp.WayPoint.Name.ToLower().Contains(searchQuery.WayPoint.ToLower())));

            result.TotalFiltered = query.Count();
            query = query.OrderBy(x => x.ScheduleItem.DepartureDate)
                .Skip(searchQuery.Paging.Skip)
                .Take(searchQuery.Paging.Length);
            result.Data = query.ProjectTo<TicketModelWithSchedule>().ToList();
            result.Paging = searchQuery.Paging;
            return result;
        }
    }
}
