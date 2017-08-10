using System;

namespace FastBus.Services.Models.User
{
    public class BaseUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateBorn { get; set; }
        public DateTime RegistredDate { get; set; }
    }

    public class UserModel : BaseUserModel
    {
        public string Role { get; set; }
    }
}
