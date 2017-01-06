using System;

namespace FastBus.Web.Models.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime RegistredDate { get; set; }
        public string Role { get; set; }
    }
}
