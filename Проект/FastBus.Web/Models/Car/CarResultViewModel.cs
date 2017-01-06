using FastBus.DAL.Objects;
namespace FastBus.Web.Models.Car
{
    public class CarResultViewModel
    {
      public CarSearchModel Search { get; set; }
      public QueryResult<CarViewModel> Result { get; set; }
    }
}
