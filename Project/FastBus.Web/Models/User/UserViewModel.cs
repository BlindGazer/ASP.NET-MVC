using System;

namespace FastBus.Web.Models.User
{
    public class BaseUserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string FullName => $"{LastName} {FirstName} {Patronymic}";
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DateBorn { get; set; }
        public DateTime RegistredDate { get; set; }
    }
    public class UserViewModel : BaseUserViewModel
    {
        public string Role { get; set; }
    }
}
