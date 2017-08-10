using System;
using FastBus.Domain.Objects;
using FastBus.Services.Models;
using FastBus.Services.Models.Route;

namespace FastBus.Services.Contracts
{
    public interface IScheduleService : IService
    {
        QueryResult<ScheduleModel> Where(ScheduleSearchQuery searchQuery);
        ScheduleModel Get(long id);
        ServiceResponse Add(ScheduleModel model, DateTime[] departureDates, DateTime[] destinationDates);
        ServiceResponse Update(ScheduleModel model);
    }
}
