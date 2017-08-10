using System;
using FastBus.Domain.Objects;

namespace FastBus.Services.Models.Driver
{
    public class DriverSearchQuery : BaseQuery
    {
        public string Name { get; set; }
        public string GovermentNumber { get; set; }
        public DateTime? RegisterDateFrom { get; set; }
        public DateTime? RegisterDateTo { get; set; }
        public DateTime? RouteDateFrom { get; set; }
        public DateTime? RouteDateTo { get; set; }
    }
}
