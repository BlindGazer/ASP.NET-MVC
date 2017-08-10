using System.ComponentModel.DataAnnotations;
namespace FastBus.Web.Models.User
{
    public class EditPasswordModel
    {
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее {2} и не более {1} символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий Пароль")]
        public string CurrentPassword { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее {2} и не более {1} символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый Пароль")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}