using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastBus.DAL.Contracts;
using FastBus.Services.Contracts;
using FastBus.Domain.Entities;
using FastBus.Domain.Objects;
using FastBus.Services.Models;
using FastBus.Services.Models.Route;

namespace FastBus.Services.Services
{
    public class ScheduleService : BaseService, IScheduleService
    {
        private readonly ICarService _carService;
        private readonly IDriverService _driverService;
        private readonly IGenericRepository<ScheduleItem> _scheduleRep;

        public ScheduleService(IUnitOfWork unitOfWork, 
            ICarService carService, IDriverService driverService) 
            : base(unitOfWork)
        {
            _carService = carService;
            _driverService = driverService;
            _scheduleRep = _uow.GetRepostirory<ScheduleItem>();
        }

        public QueryResult<ScheduleModel> Where(ScheduleSearchQuery searchQuery)
        {
            var result = new QueryResult<ScheduleModel>(_scheduleRep.All.Count());

            bool hasDestination = !string.IsNullOrWhiteSpace(searchQuery.Destination),
                hasDeparture = !string.IsNullOrWhiteSpace(searchQuery.Departure),
                hasWaypoint = !string.IsNullOrWhiteSpace(searchQuery.WayPoint);
            var currentDate = DateTime.Now.Date;

            var query = _scheduleRep.Where(c => !hasDestination || 
                    c.Route.Destination.Equals(searchQuery.Destination, StringComparison.CurrentCultureIgnoreCase),
                        c => !hasDeparture || c.Route.Departure.Equals(searchQuery.Departure, StringComparison.CurrentCultureIgnoreCase),
                        c => !searchQuery.DepartureDate.HasValue && DbFunctions.TruncateTime(c.DepartureDate) >= currentDate
                        || DbFunctions.TruncateTime(c.DepartureDate) >= searchQuery.DepartureDate,
                        c => !searchQuery.DestinationDate.HasValue || DbFunctions.TruncateTime(c.DestinationDate) <= searchQuery.DestinationDate,
                        c => !hasDeparture || c.Route.Departure.ToLower().Contains(searchQuery.Departure.ToLower()),
                        c => !hasWaypoint || 
                            c.Route.WayPoints.Any(wp => wp.WayPoint.Name.ToLower().Contains(searchQuery.WayPoint.ToLower())));
            result.TotalFiltered = query.Count();

            query = query.OrderBy(x => x.DepartureDate)
                .Skip(searchQuery.Paging.Skip)
                .Take(searchQuery.Paging.Length);
            result.Data = query.ProjectTo<ScheduleModel>().ToList();
            result.Paging = searchQuery.Paging;
            return result;
        }

        public ScheduleModel Get(long id)
        {
            return Mapper.Map<ScheduleModel>(_scheduleRep.FindById(id));
        }

        public ServiceResponse Add(ScheduleModel model, DateTime[] departureDates, DateTime[] destinationDates)
        {
            departureDates = departureDates.OrderBy(x => x).ToArray();
            destinationDates = destinationDates.OrderBy(x => x).ToArray();
            if (model == null) return new ServiceResponse(false, @"Некорректные данные");
            var driverIds = model.Drivers.Select(x => x.Id).ToArray();
            for (var i = 0; i < departureDates.Length; i++)
            {
                if (!_carService.IsFreeForDate(departureDates[i], destinationDates[i], model.CarId))
                    return new ServiceResponse(false, 
                        $@"Машина в выбранном диапозоне дат [{departureDates[i]:dd.MM.yyyy HH:mm} - {destinationDates[i]:dd.MM.yyyy HH:mm}] занята");
                if (!_driverService.IsFreeForDate(departureDates[i], destinationDates[i],  driverIds))
                    return new ServiceResponse(false, 
                        $@"Один из водителей в выбранном диапозоне дат [{departureDates[i]:dd.MM.yyyy HH:mm} - {destinationDates[i]:dd.MM.yyyy HH:mm}] занят");
            }

            var schedules = new List<ScheduleItem>();
            if (model.DispatcherId < 1)
            {
                model.DispatcherId = _uow.GetRepostirory<Dispatcher>().All.First().Id;
            } 
            var driverRep = _uow.GetRepostirory<Driver>();
            var drivers = model.Drivers.Select(x => driverRep.FindById(x.Id)).ToList();
            for(var i = 0; i < departureDates.Length; i++)
            {
                var schedule = Mapper.Map<ScheduleItem>(model);
                schedule.Drivers = drivers;
                schedule.DepartureDate = departureDates[i];
                schedule.DestinationDate = destinationDates[i];
                schedules.Add(schedule);
            }
            schedules.ForEach(_scheduleRep.Add);
            _uow.SaveChanges();
            return new ServiceResponse(true);
        }

        public ServiceResponse Update(ScheduleModel model)
        {
            if (model == null) return new ServiceResponse(false, @"Неверный код маршрута");
            var schedule = _scheduleRep.FindById(model.Id);
            if (schedule == null) return new ServiceResponse(false, @"Неверный код маршрута");
            if (schedule.Tickets.Any(x => x.IsPaid))
            {
                return new ServiceResponse(false, @"Изменить данные маршрута нельзя, уже имеются купленные билеты");
            }
            if (!_carService.IsFreeForDate(model.DestinationDate, model.DepartureDate, model.CarId, model.Id))
                return new ServiceResponse(false, @"Машина в выбранном диапозоне дат занята");
            var driverIds = model.Drivers.Select(x => x.Id).ToArray();
            if (!_driverService.IsFreeForDate(model.DestinationDate, model.DepartureDate, driverIds, model.Id))
                return new ServiceResponse(false, @"Один из водителей в выбранном диапозоне дат занят");
            schedule.RouteId = model.RouteId;
            schedule.DepartureDate = model.DepartureDate;
            schedule.DestinationDate = model.DestinationDate;
            schedule.Seats = model.Seats;
            schedule.Number = model.Number;
            schedule.CarId = model.CarId;
            if (model.DispatcherId > 0)
            {
                schedule.DispatcherId = model.DispatcherId;
            }
            schedule.Cost = model.Cost;

            _scheduleRep.Update(schedule);
            _uow.SaveChanges();
            return new ServiceResponse(true);
        }
    }
}
