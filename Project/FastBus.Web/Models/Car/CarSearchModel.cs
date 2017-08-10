using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FastBus.Domain.Objects;
using FastBus.Web.Helpers;
using FastBus.Domain.Enums;

namespace FastBus.Web.Models.Car
{
    public enum CarOrderByField
    {
        [Display(Name = "Модель")]
        Model,
        [Display(Name = "Гос номер")]
        GovermentNumber,
        [Display(Name = "Цвет")]
        Color,
        [Display(Name = "Кол-во мест")]
        Seats,
        [Display(Name = "Год выпуска")]
        Year,
        [Display(Name = "Состояние")]
        Status
    }

    public class CarSearchModel : BaseQuery
    {

        [DisplayName("Год выпуска от")]
        public short? YearFrom { get; set; }
        [DisplayName("Год выпуска до")]
        public short? YearTo { get; set; }
        [DisplayName("Модель")]
        public string Model { get; set; }
        [DisplayName("Гос номер")]
        public string GovermentNumber { get; set; }
        [DisplayName("Гаражный номер")]
        public string GarageNumber { get; set; }
        [DisplayName("Цвет")]
        public string Color { get; set; }
        [DisplayName("Водитель")]
        public string DriverName { get; set; }
        [DisplayName("Состояние")]
        public StatusCar? Status { get; set; }
        public IEnumerable<SelectListItem> OrderByFields { get; set; }

        public CarSearchModel()
        {
            OrderByFields = SelectListHelper.GetFields<CarOrderByField>();
            OrderBy = new ColumnOrder(OrderByFields.First().Value);
        }

    }
}
