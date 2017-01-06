using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FastBus.DAL.Objects;
using FastBus.Web.Helpers;

namespace FastBus.Web.Models.User
{
    public enum UserOrderByField
    {
        [Display(Name = "ФИО")]
        Name,
        [Display(Name = "Логин")]
        UserName,
        [Display(Name = "Роль")]
        Role,
        [Display(Name = "Дата регистрации")]
        RegistredDate
    }

    public class UserSearchModel : BaseQuery
    {
        [DisplayName("ФИО")]
        public string Name { get; set; }

        [DisplayName("Логин")]
        public string UserName { get; set; }

        [DisplayName("Дата регистрации от")]
        public DateTime? RegisterDateBegin { get; set; }

        [DisplayName("Дата регистрации до")]
        public DateTime? RegisterDateEnd { get; set; }

        [DisplayName("Роль")]
        public int? Role { get; set; }

        public IEnumerable<SelectListItem> OrderByFields { get; set; }

        public UserSearchModel()
        {
            var list = SelectListHelper.GetFields<UserOrderByField>();
            OrderByFields = list;
            OrderBy = new ColumnOrder
            {
                Column = list?.First().Value,
                Direction = SortDirection.Asc

            };
            Paging = new Paging();
        }

    }
}
