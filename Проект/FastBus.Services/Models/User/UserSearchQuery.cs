using System;
using FastBus.Domain.Objects;

namespace FastBus.Services.Models.User
{
    public class UserSearchQuery : BaseQuery
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime? RegisterDateFrom { get; set; }
        public DateTime? RegisterDateTo { get; set; }
        public int? Role { get; set; }
    }
}
