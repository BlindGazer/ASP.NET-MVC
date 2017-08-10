using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FastBus.Domain.Enums;
using FastBus.Domain.Objects;
using FastBus.Web.Extensions;

namespace FastBus.Web.Models.Car
{
    public class CarViewModel
    {
        public int? Id { get; set; }
        public string Name => $"{GovermentNumber} [{CarModel}] [{Color}]";
        [Required]
        [StringLength(50)]
        [Display(Name = "Модель")]
        public string CarModel { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Гос номер")]
        public string GovermentNumber { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Цвет")]
        public string Color { get; set; }
        [Required]
        [Range(1, byte.MaxValue, ErrorMessage = "Введите число")]
        [Display(Name = "Кол-во мест")]
        public byte Seats { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Введите число")]
        [Display(Name = "Гаражный номер")]
        public int? GarageNumber { get; set; }
        [Display(Name = "Год производства")]
        [RangeUntilCurrentYear(1980)]
        public short? Year { get; set; }
        [Display(Name = "Статус")]
        public StatusCar Status { get; set; }
    }
    public class AddCarViewModel : CarViewModel
    {
        [Display(Name = "Водители")]
        public int[] Drivers { get; set; }
    }
    public class CarViewModelWithDrivers : CarViewModel
    {
        public IEnumerable<ListItem> Drivers { get; set; }
    }
}
