using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using FastBus.DAL.Concrete.Entities;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.Repositories.Contracts;
using FastBus.Services.Contracts;
using FastBus.Services.Models.Car;
using FastBus.DAL.Objects;

namespace FastBus.Services.Services
{
    public class CarService : BaseService, ICarService
    {
        public CarService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public QueryResult<CarModel> Where(CarSearchQuery searchQuery)
        {
            var result = new QueryResult<CarModel>();
            var carRepo = _uow.GetRepostirory<Car>();
            result.Total = carRepo.All.Count();

            bool hasModel = !string.IsNullOrWhiteSpace(searchQuery.Model),
                hasColor = !string.IsNullOrWhiteSpace(searchQuery.Color),
                hasDriverName = !string.IsNullOrWhiteSpace(searchQuery.DriverName),
                hasGovermentNumber = !string.IsNullOrWhiteSpace(searchQuery.GovermentNumber);

            var query = carRepo.Where(c => !searchQuery.YearFrom.HasValue || c.Year >= searchQuery.YearFrom, 
                                      c => !searchQuery.YearTo.HasValue || c.Year <= searchQuery.YearTo,
                                      c => !searchQuery.Status.HasValue || c.Status == searchQuery.Status,
                                      c => !hasModel || c.Model.ToLower().Contains(searchQuery.Model.ToLower()),
                                      c => !hasGovermentNumber || c.GovermentNumber.ToLower().Contains(searchQuery.GovermentNumber.ToLower()),
                                      c => !hasDriverName || c.Drivers.Any(d => d.Name.ToLower().Contains(searchQuery.DriverName.ToLower())),
                                      c => !hasColor || c.Color.ToLower().Contains(searchQuery.Color.ToLower()));

            result.TotalFiltered = query.Count();
            query = query.OrderBy(searchQuery.OrderBy.ToString())
                .Skip(searchQuery.Paging.Skip)
                .Take(searchQuery.Paging.Length);
            result.Data = Mapper.Map<List<CarModel>>(query.ToList());
            return result;
        }

        public void Remove(int id)
        {
            _uow.GetRepostirory<Car>().Remove(id);
            _uow.SaveChanges();
        }

        public CarModel Get(int id)
        {
            return Mapper.Map<CarModel>(_uow.GetRepostirory<Car>().FindById(id));
        }
        public void Add(CarModel model)
        {
            if (model == null) return;

            var car = Mapper.Map<Car>(model);
            var driversId = car.Drivers.Select(c => c.Id);
            car.Drivers = _uow.GetRepostirory<User>().Where(u => driversId.Contains(u.Id)).ToList();
            _uow.GetRepostirory<Car>().Add(car);
            _uow.SaveChanges();
        }
        public void Update(CarModel model)
        {
            if (model == null) return;

            var car = _uow.GetRepostirory<Car>().FindById(model.Id);

            car.Drivers.Clear();
            var driversId = model.Drivers.Select(c => c.Id);
            car.Drivers = _uow.GetRepostirory<User>().Where(u => driversId.Contains(u.Id)).ToList();
            car.Model = model.Model;
            car.GovermentNumber = model.GovermentNumber;
            car.Color = model.Color;
            car.Year = model.Year;
            car.Status = model.Status;

            _uow.GetRepostirory<Car>().Update(car);
            _uow.SaveChanges();
        }
    }
}
