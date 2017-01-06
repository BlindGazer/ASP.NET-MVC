using FastBus.DAL.Objects;

namespace FastBus.Web.Models.User
{
    public class UserResultViewModel
    {
      public UserSearchModel Search { get; set; }
      public QueryResult<UserViewModel> Result { get; set; }
    }
}
