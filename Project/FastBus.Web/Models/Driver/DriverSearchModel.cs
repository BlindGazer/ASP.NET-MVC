using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FastBus.Domain.Objects;
using FastBus.Web.Helpers;

namespace FastBus.Web.Models.Driver
{
    public enum DriverOrderByField
    {
        [Display(Name = "ФИО")]
        FirstName,
        [Display(Name = "Дата регистрации")]
        RegistredDate
    }

    public class DriverSearchModel : BaseQuery
    {
        [DisplayName("ФИО")]
        public string Name { get; set; }
        [DisplayName("Гос номер машины")]
        public string GovermentNumber { get; set; }
        [DisplayName("Дата регистрации от")]
        public DateTime? RegisterDateFrom { get; set; }
        [DisplayName("Дата регистрации до")]
        public DateTime? RegisterDateTo { get; set; }
        [DisplayName("Дата маршрута от")]
        public DateTime? RouteDateFrom { get; set; }
        [DisplayName("Дата маршрута до")]
        public DateTime? RouteDateTo { get; set; }
        public IEnumerable<SelectListItem> OrderByFields { get; set; }

        public DriverSearchModel()
        {
            OrderByFields = SelectListHelper.GetFields<DriverOrderByField>();
            OrderBy = new ColumnOrder(OrderByFields.First().Value);
        }

    }
}
