using FastBus.Domain.Objects;
using FastBus.Services.Models;
using FastBus.Services.Models.User;

namespace FastBus.Services.Contracts
{
    public interface IUserService : IService
    {
        QueryResult<UserModel> Where(UserSearchQuery query);
        
        void Update(UserModel model);

        ServiceResponse Delete(string username);

    }
}
