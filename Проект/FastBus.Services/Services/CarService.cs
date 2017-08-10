using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastBus.DAL.Contracts;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Car;
using FastBus.Domain.Entities;
using FastBus.Domain.Objects;
using FastBus.Services.Models;

namespace FastBus.Services.Services
{
    public class CarService : BaseService, ICarService
    {
        private readonly IGenericRepository<Car> _carRep;

        public CarService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _carRep = _uow.GetRepostirory<Car>();
        }

        public IEnumerable<CarModel> All()
        {
            return _uow.GetRepostirory<Car>().All.Select(c => new CarModel
            {
                Id = c.Id,
                GovermentNumber = c.GovermentNumber,
                Color = c.Color,
                Model = c.Model
            }).ToList();
        }

        public QueryResult<CarModelWithDrivers> Where(CarSearchQuery searchQuery)
        {
            var result = new QueryResult<CarModelWithDrivers>(_carRep.All.Count());

            bool hasModel = !string.IsNullOrWhiteSpace(searchQuery.Model),
                hasColor = !string.IsNullOrWhiteSpace(searchQuery.Color),
                hasDriverName = !string.IsNullOrWhiteSpace(searchQuery.DriverName),
                hasGarageNumber = !string.IsNullOrWhiteSpace(searchQuery.GarageNumber),
                hasGovermentNumber = !string.IsNullOrWhiteSpace(searchQuery.GovermentNumber);

            var query = _carRep.Where(c => !searchQuery.YearFrom.HasValue || c.Year >= searchQuery.YearFrom, 
                                      c => !searchQuery.YearTo.HasValue || c.Year <= searchQuery.YearTo,
                                      c => !searchQuery.Status.HasValue || c.Status == searchQuery.Status,
                                      c => !hasModel || c.Model.ToLower().Contains(searchQuery.Model.ToLower()),
                                      c => !hasGovermentNumber || 
                                        c.GovermentNumber.ToLower().Contains(searchQuery.GovermentNumber.ToLower()),
                                      c => !hasGarageNumber || c.GarageNumber.ToString().Contains(searchQuery.GarageNumber),
                                      c => !hasDriverName ||  
                                        c.Drivers.Any(d => (d.FirstName + " " + d.LastName + " " + d.Patronymic).
                                            ToLower().Contains(searchQuery.DriverName.ToLower())) || 
                                        c.Drivers.Any(d => (d.LastName + " " + d.FirstName).ToLower().
                                            Contains(searchQuery.DriverName.ToLower())),
                                      c => !hasColor || c.Color.ToLower().Contains(searchQuery.Color.ToLower()));

            result.TotalFiltered = query.Count();

            query = query.OrderBy(searchQuery.OrderBy.ToString())
                .Skip(searchQuery.Paging.Skip)
                .Take(searchQuery.Paging.Length);

            result.Data = query.ProjectTo<CarModelWithDrivers>().ToList();
            result.Paging = searchQuery.Paging;

            return result;
        }

        public ServiceResponse Delete(int id)
        {
            var car = _carRep.FindById(id);
            if (car == null)
            {
                return new ServiceResponse(false, "Неверный идентификатор");
            }
            if (car.CustomRoutes.Any() || car.Schedule.Any())
            {
                return new ServiceResponse(false, "Удаление невозможно, машина закреплен за маршрутом");
            }
            _carRep.Remove(car);
            _uow.SaveChanges();
            return new ServiceResponse(true);
        }

        public CarModelWithDrivers Get(int id)
        {
            return Mapper.Map<CarModelWithDrivers>(_carRep.FindById(id));
        }

        public void Add(CarModelWithDrivers model)
        {
            if (model == null) return;

            var car = Mapper.Map<Car>(model);
            var driversId = car.Drivers.Select(c => c.Id);
            car.Drivers = _uow.GetRepostirory<Driver>().Where(d => driversId.Contains(d.Id)).ToList();
            _carRep.Add(car);
            _uow.SaveChanges();
        }

        public void Update(CarModelWithDrivers model)
        {
            if (model == null) return;

            var car = _carRep.FindById(model.Id);

            car.Drivers.Clear();
            var driversId = model.Drivers.Select(c => c.Id);

            car.Drivers = _uow.GetRepostirory<Driver>().Where(d => driversId.Contains(d.Id)).ToList();
            car.Model = model.Model;
            car.GovermentNumber = model.GovermentNumber;
            car.Color = model.Color;
            car.Year = model.Year;
            car.Status = model.Status;
            car.Seats = model.Seats;

            _carRep.Update(car);
            _uow.SaveChanges();
        }

        public bool IsFreeForDate(DateTime departureDate, DateTime destinationDate, int carId, long? scheduleId = null)
        {
            var scheduleRep = _uow.GetRepostirory<ScheduleItem>();
            return !scheduleRep.All.Any(x => 
                (!scheduleId.HasValue || x.Id != scheduleId.Value) && x.CarId == carId && 
                ( departureDate <= x.DepartureDate && destinationDate >= x.DepartureDate ||
                 departureDate >= x.DepartureDate && destinationDate <= x.DestinationDate ||
                  departureDate <= x.DestinationDate && destinationDate >= x.DestinationDate));
        }
    }
}
