using System;
using System.ComponentModel.DataAnnotations;

namespace FastBus.Web.Models.User
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }

    public class BaseEditUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Имя")]
        [StringLength(25, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(25, MinimumLength = 2)]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        [StringLength(25, MinimumLength = 2)]
        public string Patronymic { get; set; }
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateBorn { get; set; }
    }

    public class EditUserViewModel : BaseEditUserViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "Логин")]
        public string UserName { get; set; }
    }

    public class RegisterUserViewModel : BaseEditUserViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее {2} и не более {1} символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserViewModelWithRole : RegisterUserViewModel
    {
        [Required]
        [Display(Name = "Роль")]
        public string UserRole { get; set; }
    }

    public class UserRegisterResponse
    {
        public bool IsSuccessfull { get; set; }
        public string Message { get; set; }

        public UserRegisterResponse(string message)
        {
            IsSuccessfull = true;
            Message = message;
        }

        public UserRegisterResponse(bool status, string message = null) :this(message)
        {
            IsSuccessfull = status;
        }
    }
}