using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FastBus.Web.Attributes;

namespace FastBus.Web.Models.Car
{
    public class AddCarViewModel
    {
        public int? Id { get; set; }
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
        [Range(1, int.MaxValue, ErrorMessage = "Введите число")]
        [Display(Name = "Кол-во мест")]
        public int Seats { get; set; }
        [Display(Name = "Год производства")]
        [RangeUntilCurrentYear(1980)]
        public short? Year { get; set; }
        [Display(Name = "Водители")]
        public IEnumerable<int> Drivers { get; set; }
    }
}
