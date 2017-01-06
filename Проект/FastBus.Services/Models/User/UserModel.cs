using System;

namespace FastBus.Services.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime RegistredDate { get; set; }
        public string Role { get; set; }
    }
}
