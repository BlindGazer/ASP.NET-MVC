using FastBus.Domain.Objects;
using FastBus.Services.Models.Route;

namespace FastBus.Services.Contracts
{
    public interface ITicketsService : IService
    {
        QueryResult<TicketModelWithSchedule> Where(TicketSearchQuery searchQuery);
    }
}
