using FastBus.DAL.Objects;
using FastBus.Services.Models.Car;

namespace FastBus.Services.Contracts
{
    public interface ICarService : IService
    {
        QueryResult<CarModel> Where(CarSearchQuery query);
        void Remove(int id);
        CarModel Get(int id);
        void Add(CarModel model);
        void Update(CarModel model);
    }
}
