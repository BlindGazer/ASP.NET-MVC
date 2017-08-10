using System.ComponentModel.DataAnnotations;
using FastBus.Web.Models.User;

namespace FastBus.Web.Models.Driver
{
    public class EditDriverViewModel : EditUserViewModel
    {
        [Display(Name = "Машины")]
        public int[] Cars { get; set; }
    }
}
