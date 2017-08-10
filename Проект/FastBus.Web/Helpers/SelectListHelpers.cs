using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FastBus.DAL.Contracts;
using FastBus.Domain.Entities.Identity;
using FastBus.Domain.Enums;
using FastBus.Services.Contracts;

namespace FastBus.Web.Helpers
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetRoles(bool withId = false)
        {
            return DependencyResolver.Current.GetService<IUnitOfWork>().GetRepostirory<Role>()
                .Where(r => r.Name != UserRoles.Buyer).OrderBy(n => n.Name)
                .Select(n => new SelectListItem
                {
                    Text = n.Description,
                    Value = withId ? n.Id.ToString() : n.Name
                }).ToList();
        }

        public static List<SelectListItem> GetFields<TEntity>()
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

        public static List<SelectListItem> GetDrivers(int? carId = null)
        {
            return DependencyResolver.Current.GetService<IDriverService>().All(carId)
                .Select(d => new SelectListItem
                {
                    Text = $@"{d.FirstName} {d.LastName} {d.Patronymic}",
                    Value = d.Id.ToString()
                }).ToList();
        }

        public static List<SelectListItem> GetCars()
        {
            return DependencyResolver.Current.GetService<ICarService>().All()
                .Select(d => new SelectListItem
                {
                    Text = $@"{d.GovermentNumber} [{d.Model}] [{d.Color}]",
                    Value = d.Id.ToString()
                }).ToList();
        }

        public static List<SelectListItem> GetCarStatuses()
        {
            return DependencyResolver.Current.GetService<ICarService>().All()
                .Select(d => new SelectListItem
                {
                    Text = $@"{d.GovermentNumber} [{d.Model}] [{d.Color}]",
                    Value = d.Id.ToString()
                }).ToList();
        }

        public static List<SelectListItem> GetRoutes()
        {
            return DependencyResolver.Current.GetService<IRouteService>().All()
                .Select(r => new SelectListItem
                {
                    Text = $@"{r.Departure} -> {r.Destination}",
                    Value = r.Id.ToString()
                }).ToList();
        }

        public static List<SelectListItem> GetTicketsTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = @"Все",
                    Value = null
                },
                new SelectListItem
                {
                    Text = @"Купленные",
                    Value = "True"
                },
                new SelectListItem
                {
                    Text = @"Забронированные",
                    Value = "False"
                }
            };
        }
    }
}