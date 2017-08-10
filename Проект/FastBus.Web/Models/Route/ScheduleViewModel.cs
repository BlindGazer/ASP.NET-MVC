using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FastBus.Domain.Objects;
using FastBus.Web.Models.Car;

namespace FastBus.Web.Models.Route
{
    public class BaseScheduleViewModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Номер рейса")]
        [Range(1, int.MaxValue, ErrorMessage = "Некорректный номер рейса")]
        public int Number { get; set; }
        [Required]
        [DisplayName("Кол-во мест")]
        public byte Seats { get; set; }
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }
    }

    public class ScheduleViewModel : BaseScheduleViewModel
    {
        public RouteViewModel Route { get; set; }
        public CarViewModel Car { get; set; }
        [DisplayName("День отправления")]
        public DateTime DepartureDate { get; set; }
        [DisplayName("Время прибытия")]
        public DateTime DestinationDate { get; set; }
        public string DispatcherName { get; set; }
        public int PayTickets { get; set; }
        public int ReserveTickets { get; set; }
        public List<ListItem> Drivers { get; set; }

        [DisplayName("Время")]
        public string DepartureTime => DepartureDate.ToShortTimeString();
        public TimeSpan InTransitTime => DestinationDate - DepartureDate;
        [DisplayName("Время в пути")]
        public string InTransit => $"{InTransitTime.Hours}:{InTransitTime.Minutes:00}";
        public int FreeSeats => Seats - PayTickets - ReserveTickets;
        [DisplayName("Водитель")]
        public string DriversNames => string.Join(", ", Drivers.Select(x => x.Name));
    }

    public class BaseScheduleEditModel : BaseScheduleViewModel
    {
        [Required]
        [DisplayName("Маршрут")]
        public int RouteId { get; set; }
        [Required]
        [DisplayName("Часы")]
        [Range(0,23)]
        public byte DepartureHours { get; set; }
        [Required]
        [DisplayName("Минуты")]
        [Range(0, 59)]
        public byte DepartureMinutes { get; set; }
        [Required]
        [DisplayName("Часы")]
        [Range(0, 23)]
        public byte DestinationHours { get; set; }
        [Required]
        [DisplayName("Минуты")]
        [Range(0, 59)]
        public byte DestinationMinutes { get; set; }
        [Required]
        [DisplayName("Машина")]
        public int CarId { get; set; }
        public int DispatcherId { get; set; }
        [Required]
        [DisplayName("Водители")]
        public int[] Drivers { get; set; }
    }

    public class ScheduleAddModel : BaseScheduleEditModel
    {
        [Required]
        [DisplayName("Даты отправления")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime[] DepartureDates { get; set; }
        [Required]
        [DisplayName("Даты назначения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", ApplyFormatInEditMode = true)]
        public DateTime[] DestinationDates { get; set; }

        public bool InitialDatesIsValid(ModelStateDictionary state)
        {
            bool isValid = true;
            if (DepartureDates.Length < 1)
            {
                state.AddModelError("", @"Вы не указали дату отправления или дату прибытия");
                isValid = false;
            }
            if (DepartureDates.Length != DestinationDates.Length)
            {
                state.AddModelError("", @"Количество дат отправления, и дат прибытия не совпадает");
                isValid = false;
            }
            if (!isValid) return false;

            DepartureDates = DepartureDates.Select(x => x.AddHours(DepartureHours).AddMinutes(DepartureMinutes)).ToArray();
            DestinationDates = DestinationDates.Select(x => x.AddHours(DestinationHours).AddMinutes(DepartureMinutes)).ToArray();
            return true;
        }

        public string GetDepartureDates()
        {
            return DepartureDates != null ? string.Join(", ", DepartureDates.Select(x => x.ToString("yyyy-MM-dd"))) : "";
        }

        public string GetDestinationDates()
        {
            return DestinationDates != null ? string.Join(", ", DestinationDates.Select(x => x.ToString("yyyy-MM-dd"))) : "";
        }
    }

    public class ScheduleEditModel : BaseScheduleEditModel
    {
        [Required]
        [DisplayName("Дата отправления")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDate { get; set; }
        [Required]
        [DisplayName("Дата назначения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DestinationDate { get; set; }
    }
}
