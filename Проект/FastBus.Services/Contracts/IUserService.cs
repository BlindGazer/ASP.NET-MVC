using System.Collections.Generic;
using FastBus.DAL.Objects;
using FastBus.Services.Models.User;

namespace FastBus.Services.Contracts
{
    public interface IUserService : IService
    {
        QueryResult<UserModel> Where(UserSearchQuery query);
        IEnumerable<UserModel> GetDrivers();
        void Remove(int id);

    }
}
