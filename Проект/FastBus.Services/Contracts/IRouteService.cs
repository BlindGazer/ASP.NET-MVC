using System.Collections.Generic;
using FastBus.Domain.Objects;
using FastBus.Services.Models;
using FastBus.Services.Models.Route;

namespace FastBus.Services.Contracts
{
    public interface IRouteService : IService
    {
        IEnumerable<RouteModel> All();
        QueryResult<RouteModel> Where(RouteSearchQuery searchQuery);
        ServiceResponse Delete(int id);
        RouteModel Get(int id);
        IEnumerable<string> GetDepartures(string departure);
        IEnumerable<string> GetDestination(string departure, string destination);
        void Add(RouteModel model);
        void Update(RouteModel model);
    }
}
