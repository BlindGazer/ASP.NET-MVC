using System;
using FastBus.DAL.Objects;

namespace FastBus.Services.Models.User
{
    public class UserSearchQuery : BaseQuery
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime? RegisterDateBegin { get; set; }
        public DateTime? RegisterDateEnd { get; set; }
        public int? Role { get; set; }

    }
}
