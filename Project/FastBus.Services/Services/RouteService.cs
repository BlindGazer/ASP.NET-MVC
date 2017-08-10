using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
    public class RouteService : BaseService, IRouteService
    {
        private readonly IGenericRepository<Route> _routeRep;

        public RouteService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _routeRep = _uow.GetRepostirory<Route>();
        }

        public IEnumerable<RouteModel> All()
        {
            return _routeRep.All.Select(r => new RouteModel
            {
                Id = r.Id,
                Departure = r.Departure,
                Destination = r.Destination
            }).ToList();
        }

        public QueryResult<RouteModel> Where(RouteSearchQuery searchQuery)
        {
            var result = new QueryResult<RouteModel>(_routeRep.All.Count());

            bool hasDestination = !string.IsNullOrWhiteSpace(searchQuery.Destination),
                hasDeparture = !string.IsNullOrWhiteSpace(searchQuery.Departure),
                hasWaypoint = !string.IsNullOrWhiteSpace(searchQuery.WayPoint);

            var query = _routeRep.Where(c => !hasDestination || c.Destination.ToLower().Contains(searchQuery.Destination.ToLower()),
                        c => !hasDeparture || c.Departure.ToLower().Contains(searchQuery.Departure.ToLower()),
                        c => !hasWaypoint || c.WayPoints.Any(wp => wp.WayPoint.Name.ToLower().Contains(searchQuery.WayPoint.ToLower())));

            result.TotalFiltered = query.Count();
            query = query.OrderBy(searchQuery.OrderBy.ToString())
                .Skip(searchQuery.Paging.Skip)
                .Take(searchQuery.Paging.Length);
            result.Data = query.ProjectTo<RouteModel>().ToList();
            result.Data.ForEach(x =>
            {
                x.WayPoints = x.WayPoints?.OrderBy(wp => wp.Order);
            });
            result.Paging = searchQuery.Paging;
            return result;
        }

        public IEnumerable<string> GetDepartures(string departure)
        {
            var isEmptyDeparture = string.IsNullOrWhiteSpace(departure);
            departure = isEmptyDeparture ? null : departure.Replace(" ", "").ToLower();
            return _routeRep.Where(x => isEmptyDeparture || 
                    x.Departure.Replace(" ", "").ToLower().Contains(departure)).
                    Select(x => x.Departure).Distinct().OrderBy(x => x);
        }

        public IEnumerable<string> GetDestination(string departure, string destination)
        {
            bool isEmptyDeparture = string.IsNullOrWhiteSpace(departure),
                isEmptyDestination = string.IsNullOrWhiteSpace(destination);
            departure = isEmptyDeparture ? null : departure.Replace(" ", "").ToLower();
            destination = isEmptyDestination ? null : destination.Replace(" ", "").ToLower();
            return _routeRep.Where(x => isEmptyDeparture || x.Departure.Replace(" ", "").ToLower().Contains(departure),
                    x => isEmptyDestination || x.Destination.Replace(" ", "").ToLower().Contains(destination)).
                    Select(x => x.Destination).Distinct().OrderBy(x => x);
        }

        public ServiceResponse Delete(int id)
        {
            var route = _routeRep.FindById(id);
            if (route == null)
            {
                return new ServiceResponse(false, "Неверный идентификатор");
            }
            if (route.Schedule.Any())
            {
                return new ServiceResponse(false, "Удаление невозможно, маршрут закреплен за расписанием");
            }
            _routeRep.Remove(route);
            _uow.SaveChanges();
            return new ServiceResponse(true);
        }

        public RouteModel Get(int id)
        {
            return Mapper.Map<RouteModel>(_routeRep.FindById(id));
        }

        public void Add(RouteModel model)
        {
            if (model == null) return;
            _routeRep.Add(Mapper.Map<Route>(model));
            _uow.SaveChanges();
        }

        public void Update(RouteModel model)
        {
            if (model == null) return;
            var route = _routeRep.FindById(model.Id);
            if(route == null) return;

            route.Departure = model.Departure;
            route.Destination = model.Destination;
            var wayPointRep = _uow.GetRepostirory<WayPoint>();
            route.WayPoints.ToList().ForEach(wayPointRep.Remove);
            route.WayPoints = Mapper.Map<List<RouteWayPoint>>(model.WayPoints);
            _routeRep.Update(route);
            _uow.SaveChanges();
        }
    }
}
