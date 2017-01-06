using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Enums;
using FastBus.Repositories.Contracts;
using FastBus.Services.Contracts;
using Microsoft.Practices.ObjectBuilder2;

namespace FastBus.Web.Helpers
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetRoles(bool withId = false)
        {
            return DependencyResolver.Current.GetService<IUnitOfWork>().GetRepostirory<Role>()
                .Where(r => r.Name != UserRoles.Client).OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Text = n.Description,
                    Value = withId ? n.Id.ToString() : n.Name
                }).ToList();
        }
        public static IEnumerable<SelectListItem> GetFields<TEntity>()
        {
            var fieldsList = new List<SelectListItem>();
            var properties = typeof(TEntity).GetProperties();
            var fields = typeof(TEntity).GetFields();
            if (properties.Length > 0)
            {
                foreach (PropertyInfo field in properties)
                {
                    fieldsList.Add(new SelectListItem
                    {
                        Text = field.IsDefined(typeof(DisplayAttribute))
                        ? field.GetCustomAttributes(typeof(DisplayAttribute)).Cast<DisplayAttribute>().Single().Name
                        : field.IsDefined(typeof(DescriptionAttribute))
                            ? field.GetCustomAttributes(typeof(DescriptionAttribute)).Cast<DescriptionAttribute>().Single().Description
                            : field.Name,
                        Value = field.Name
                    });
                }
            }
            else if (fields.Length > 0)
            {
                foreach (FieldInfo field in fields)
                {
                    if (field.Name == "value__") continue;
                    fieldsList.Add(new SelectListItem
                    {
                        Text = field.IsDefined(typeof(DisplayAttribute))
                        ? field.GetCustomAttributes(typeof(DisplayAttribute)).Cast<DisplayAttribute>().Single().Name
                        : field.IsDefined(typeof(DescriptionAttribute))
                            ? field.GetCustomAttributes(typeof(DescriptionAttribute)).Cast<DescriptionAttribute>().Single().Description
                            : field.Name,
                        Value = field.Name
                    });
                }
            }
            return fieldsList;
        }

        public static List<SelectListItem> GetDrivers()
        {
            return DependencyResolver.Current.GetService<IUserService>().GetDrivers()
                .Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString(),
                }).ToList();
        }
    }
}