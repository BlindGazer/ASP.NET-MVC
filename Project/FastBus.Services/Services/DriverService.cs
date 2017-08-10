using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper.QueryableExtensions;
using FastBus.DAL.Contracts;
using FastBus.Domain.Entities;
using FastBus.Domain.Objects;
using FastBus.Services.Contracts;
using FastBus.Services.Models;
using FastBus.Services.Models.Driver;

namespace FastBus.Services.Services
{
    public class DriverService : BaseService, IDriverService
    {
        private readonly IGenericRepository<Driver> _driverRep;
        public DriverService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _driverRep = _uow.GetRepostirory<Driver>();
        }
        
        public IEnumerable<DriverModel> All(int? cardId = null)
        {
            return _driverRep.Where(x => !cardId.HasValue || x.Cars.Any(c => c.Id == cardId.Value)).
                Select(d => new DriverModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    Patronymic = d.Patronymic
                }).ToList();
        }
        
        public QueryResult<DriverModel> Where(DriverSearchQuery searchQuery)
        {
            var result = new QueryResult<DriverModel>(_driverRep.All.Count());
            if (!searchQuery.RegisterDateTo.HasValue)
            {
                searchQuery.RegisterDateTo = DateTime.Now;
            }
            bool hasName = !string.IsNullOrWhiteSpace(searchQuery.Name),
                hasNumber = !string.IsNullOrWhiteSpace(searchQuery.GovermentNumber);

            var drivers = _driverRep.Where(d => !hasName ||
                                    (d.FirstName + " " + d.LastName + " " + d.Patronymic).ToLower().Contains(searchQuery.Name.ToLower()) ||
                                    (d.LastName + " " + d.FirstName + " " + d.Patronymic).ToLower().Contains(searchQuery.Name.ToLower()),
                                   d => !hasNumber || d.Cars.Any(x => x.GovermentNumber.Contains(searchQuery.GovermentNumber.ToLower())),
                                   d => !searchQuery.RouteDateTo.HasValue || 
                                    d.Schedule.Any(x => x.DepartureDate <= searchQuery.RouteDateTo.Value),
                                   d => !searchQuery.RouteDateFrom.HasValue || 
                                    d.Schedule.Any(x => x.DepartureDate >= searchQuery.RouteDateFrom.Value),
                                   d => !searchQuery.RegisterDateFrom.HasValue && d.RegistredDate <= searchQuery.RegisterDateTo.Value ||
                                   d.RegistredDate >= searchQuery.RegisterDateFrom.Value && 
                                    d.RegistredDate <= searchQuery.RegisterDateTo.Value);

            result.TotalFiltered = drivers.Count();

            drivers = drivers.OrderBy(searchQuery.OrderBy.ToString()).
                Skip(searchQuery.Paging.Skip).
                Take(searchQuery.Paging.Length);

            result.Paging = searchQuery.Paging;
            result.Data = drivers.ProjectTo<DriverModel>().ToList();
            return result;
        }

        public void Update(DriverModel model)
        {
            if (model == null) return;

            var driver = _driverRep.FindById(model.Id);

            driver.Cars.Clear();
            var carsId = model.Cars.Select(c => c.Id);

            driver.Cars = _uow.GetRepostirory<Car>().Where(d => carsId.Contains(d.Id)).ToList();
            driver.FirstName = model.FirstName;
            driver.LastName = model.LastName;
            driver.Patronymic = model.Patronymic;
            driver.DateBorn = model.DateBorn?.Year >= 1900 ? model.DateBorn : null;
            driver.Email = model.Email;

            _driverRep.Update(driver);
            _uow.SaveChanges();
        }

        public ServiceResponse Delete(string username)
        {
            var response = new ServiceResponse(false, @"Неверный логин");
            if (string.IsNullOrWhiteSpace(username))
            {
                return response;
            }

            var driver = _driverRep.All.FirstOrDefault(x => x.UserName.Contains(username));
            if (driver == null)
            {
                return response;
            }
            if (driver.CustomRoutes.Any() || driver.Schedule.Any())
            {
                return response.SetMessage("Удаление невозможно, пользователь закреплен за маршрутом");
            }
            driver.Roles.Clear();
            _driverRep.Remove(driver);
            _uow.SaveChanges();

            return new ServiceResponse(true);
        }
        
        public bool IsFreeForDate(DateTime departureDate, DateTime destinationDate, int[] driverIds, long? scheduleId = null)
        {
            var scheduleRep = _uow.GetRepostirory<ScheduleItem>();
            return !scheduleRep.All.Any(x => 
                (!scheduleId.HasValue || x.Id != scheduleId) &&  
                x.Drivers.Any(d => driverIds.Any(id => id == d.Id)) &&
                (departureDate <= x.DepartureDate && destinationDate >= x.DepartureDate ||
                departureDate >= x.DepartureDate && destinationDate <= x.DestinationDate ||
                departureDate <= x.DestinationDate && destinationDate >= x.DestinationDate));
        }
    }
}
