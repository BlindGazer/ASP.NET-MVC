using FastBus.Repositories.Contracts;

namespace FastBus.Services.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork _uow;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
    }
}
