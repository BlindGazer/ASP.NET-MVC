using System.Collections.Generic;
using FastBus.Services.Models.Route;
using FastBus.Services.Models.User;

namespace FastBus.Services.Models.Buyer
{
    public class BuyerModel : BaseUserModel
    {
        public List<TicketModel> Tickets { get; set; }
    }
}
