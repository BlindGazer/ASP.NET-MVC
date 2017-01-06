using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FastBus.DAL.Enums;
using FastBus.DAL.Objects;
using FastBus.Web.Helpers;

namespace FastBus.Web.Models.Car
{
    public class CarSearchModel : BaseQuery
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

        [DisplayName("Год выпуска от")]
        public short? YearFrom { get; set; }
        [DisplayName("Год выпуска до")]
        public short? YearTo { get; set; }
        [DisplayName("Модель")]
        public string Model { get; set; }
        [DisplayName("Гос номер")]
        public string GovermentNumber { get; set; }
        [DisplayName("Цвет")]
        public string Color { get; set; }
        [DisplayName("Водитель")]
        public string DriverName { get; set; }
        [DisplayName("Состояние")]
        public StatusCar? Status { get; set; }

        public IEnumerable<SelectListItem> OrderByFields { get; set; }

        public CarSearchModel()
        {
            var list = SelectListHelper.GetFields<CarOrderByField>();
            OrderByFields = list;
            OrderBy = new ColumnOrder
            {
                Column = list.First().Value,
                Direction = SortDirection.Asc

            };
            Paging = new Paging();
        }

    }
}
