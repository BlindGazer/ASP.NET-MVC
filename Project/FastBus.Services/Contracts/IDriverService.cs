using System;
using System.Collections.Generic;
using FastBus.Domain.Objects;
using FastBus.Services.Models;
using FastBus.Services.Models.Driver;

namespace FastBus.Services.Contracts
{
    public interface IDriverService : IService
    {
        IEnumerable<DriverModel> All(int? cardId = null);
        ServiceResponse Delete(string username);
        void Update(DriverModel model);
        QueryResult<DriverModel> Where(DriverSearchQuery searchQuery);
        bool IsFreeForDate(DateTime destinationDate, DateTime departureDate, int[] driverIds, long? scheduleId = null);
    }
}
