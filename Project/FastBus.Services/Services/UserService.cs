using System;
using System.Linq;
using System.Linq.Dynamic;
using FastBus.DAL.Contracts;
using FastBus.Domain.Entities;
using FastBus.Services.Contracts;
using FastBus.Services.Models.User;
using FastBus.Domain.Entities.Identity;
using FastBus.Domain.Enums;
using FastBus.Domain.Objects;
using FastBus.Services.Models;

namespace FastBus.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IGenericRepository<User> _userRep;

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRep = _uow.GetRepostirory<User>();
        }

        public QueryResult<UserModel> Where(UserSearchQuery searchQuery)
        {
            var result = new QueryResult<UserModel>();
            var roleRep = _uow.GetRepostirory<Role>();
            var clientRoleId = roleRep.Get(x => x.Name == UserRoles.Buyer).Id;
            if (!searchQuery.RegisterDateTo.HasValue)
            {
                searchQuery.RegisterDateTo = DateTime.Now;
            }
            bool hasName = !string.IsNullOrWhiteSpace(searchQuery.Name),
                hasUserName = !string.IsNullOrWhiteSpace(searchQuery.UserName);
            var users = _userRep.All.Where(u => u.Roles.All(r => r.RoleId != clientRoleId));

            result.Total = users.Count();
            users = users.Where(u => (!hasName ||
                                    (u.FirstName + " " + u.LastName + " " + u.Patronymic).ToLower().Contains(searchQuery.Name.ToLower()) ||
                                    (u.LastName + " " + u.FirstName).ToLower().Contains(searchQuery.Name.ToLower())) &&
                                   (!hasUserName || u.UserName.ToLower().Contains(searchQuery.UserName.ToLower())) &&
                                    (!searchQuery.Role.HasValue || u.Roles.Any(r => r.RoleId == searchQuery.Role)) &&
                                  (!searchQuery.RegisterDateFrom.HasValue && u.RegistredDate <= searchQuery.RegisterDateTo.Value ||
                                   u.RegistredDate >= searchQuery.RegisterDateFrom.Value && 
                                   u.RegistredDate <= searchQuery.RegisterDateTo.Value));

            var query = from u in users
                        from ur in u.Roles
                        join r in roleRep.All on ur.RoleId equals r.Id
                        select new UserModel
                        {
                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            UserName = u.UserName,
                            RegistredDate = u.RegistredDate,
                            Role = r.Description
                        };

            result.TotalFiltered = query.Count();

            query = query.OrderBy(searchQuery.OrderBy.ToString()).
                Skip(searchQuery.Paging.Skip).
                Take(searchQuery.Paging.Length);

            result.Paging = searchQuery.Paging;
            result.Data = query.ToList();

            return result;
        }

        public void Update(UserModel model)
        {
            if (model == null) return;

            var user = _userRep.FindById(model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Patronymic = model.Patronymic;
            user.DateBorn = model.DateBorn?.Year >= 1900 ? model.DateBorn : null;
            user.Email = model.Email;

            _userRep.Update(user);
            _uow.SaveChanges();
        }

        public ServiceResponse Delete(string username)
        {
            var response = new ServiceResponse(false, "Неверный логин");
            if (string.IsNullOrWhiteSpace(username))
            {
                return response;
            }

            var user = _userRep.All.SingleOrDefault(x => x.UserName.Equals(username));

            if (user == null)
            {
                return response;
            }
            var roleRep = _uow.GetRepostirory<Role>();
            int adminRoleId = roleRep.Get(x => x.Name == UserRoles.Admin).Id;
            int dispatcherRoleId = roleRep.Get(x => x.Name == UserRoles.Dispatcher).Id;

            if (user.Roles.All(x => x.RoleId == adminRoleId))
            {
                user.Roles.Clear();
                _userRep.Remove(user);
            }
            else if (user.Roles.All(x => x.RoleId == dispatcherRoleId))
            {
                if (!DeleteDispatcher(username, response))
                {
                    return response;
                }
            }
            else
            {
                return response.SetMessage("Невозможно удалить пользователя используя данный сервис");
            }
            _uow.SaveChanges();

            return new ServiceResponse(true);
        }

        private bool DeleteDispatcher(string username, ServiceResponse response)
        {
            var dispatcher = _uow.GetRepostirory<Dispatcher>().All.SingleOrDefault(x => x.UserName.Equals(username));
            if (dispatcher == null)
            {
                return false;
            }
            if (dispatcher.CustomRoutes.Any() || dispatcher.Schedule.Any())
            {
               response.SetMessage("Удаление невозможно, пользователь закреплен за маршрутом");
               return false;
            }
            dispatcher.Roles.Clear();
            _uow.GetRepostirory<Dispatcher>().Remove(dispatcher);
            return true;
        }
    }

}
