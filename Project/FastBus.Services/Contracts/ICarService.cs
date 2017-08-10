using System;
using System.Collections.Generic;
using FastBus.Domain.Objects;
using FastBus.Services.Models;
using FastBus.Services.Models.Car;

namespace FastBus.Services.Contracts
{
    public interface ICarService : IService
    {
        IEnumerable<CarModel> All();
        QueryResult<CarModelWithDrivers> Where(CarSearchQuery searchQuery);
        ServiceResponse Delete(int id);
        CarModelWithDrivers Get(int id);
        void Add(CarModelWithDrivers model);
        void Update(CarModelWithDrivers model);
        bool IsFreeForDate(DateTime departureDate, DateTime destinationDate, int carId, long? scheduleId = null);
    }
}
