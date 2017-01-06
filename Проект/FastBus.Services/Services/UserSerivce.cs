using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.Repositories.Contracts;
using FastBus.Services.Contracts;
using FastBus.Services.Models.User;
using FastBus.DAL.Enums;
using FastBus.DAL.Objects;

namespace FastBus.Services.Services
{
    public class UserSerivce : BaseService, IUserService
    {
        public UserSerivce(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public QueryResult<UserModel> Where(UserSearchQuery searchQuery)
        {
            var result = new QueryResult<UserModel>();
            var clientRoleId = _uow.GetRepostirory<Role>().Get(x => x.Name == UserRoles.Client).Id;
            if (!searchQuery.RegisterDateEnd.HasValue)
            {
                searchQuery.RegisterDateEnd = DateTime.Now;
            }
            bool hasName = !string.IsNullOrWhiteSpace(searchQuery.Name), 
                hasUserName = !string.IsNullOrWhiteSpace(searchQuery.UserName);

            var userRepo = _uow.GetRepostirory<User>();
            result.Total = userRepo.All.Count();
            var users = userRepo.Where(u => !hasName || u.Name.ToLower().Contains(searchQuery.Name.ToLower()),
                                   u => !hasUserName || u.UserName.ToLower().Contains(searchQuery.UserName.ToLower()),
                                   u => !searchQuery.Role.HasValue && u.Roles.All(r => r.RoleId != clientRoleId) || u.Roles.Any(r => r.RoleId == searchQuery.Role),
                                   u => (!searchQuery.RegisterDateBegin.HasValue && u.RegistredDate <= searchQuery.RegisterDateEnd.Value) ||
                                   (u.RegistredDate >= searchQuery.RegisterDateBegin.Value && u.RegistredDate <= searchQuery.RegisterDateEnd.Value));

            var query = from u in users
                        from ur in u.Roles
                        join r in _uow.GetRepostirory<Role>().All on ur.RoleId equals r.Id
                        select new UserModel
                        {
                            Id = u.Id,
                            Name = u.Name,
                            UserName = u.UserName,
                            RegistredDate = u.RegistredDate,
                            Role = r.Description
                        };

            result.TotalFiltered = query.Count();
            query = query.OrderBy(searchQuery.OrderBy.ToString())
                .Skip(searchQuery.Paging.Skip)
                .Take(searchQuery.Paging.Length);
            result.Data = query.ToList();
            return result;
        }

        public IEnumerable<UserModel> GetDrivers()
        {
            var driverRoleId = _uow.GetRepostirory<Role>().Get(x => x.Name == UserRoles.Driver).Id;
            var result = _uow.GetRepostirory<User>().Where(u => u.Roles.Any(r => r.RoleId == driverRoleId)).ToList();
            return Mapper.Map<IEnumerable<UserModel>>(result.ToList());
        }

        public void Remove(int userId)
        {
            _uow.GetRepostirory<User>().Remove(userId);
            _uow.SaveChanges();
        }
    }
}
