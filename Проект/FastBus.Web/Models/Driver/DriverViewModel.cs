using System.Collections.Generic;
using FastBus.Web.Models.Car;
using FastBus.Web.Models.User;

namespace FastBus.Web.Models.Driver
{
    public class DriverViewModel : BaseUserViewModel
    {
        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
