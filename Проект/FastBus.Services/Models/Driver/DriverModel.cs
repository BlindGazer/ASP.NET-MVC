using System.Collections.Generic;
using FastBus.Services.Models.Car;
using FastBus.Services.Models.User;

namespace FastBus.Services.Models.Driver
{
    public class DriverModel : BaseUserModel
    {
        public IEnumerable<CarModel> Cars { get; set; }
    }
}
