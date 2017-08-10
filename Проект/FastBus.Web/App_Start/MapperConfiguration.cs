using System;
using System.Linq;
using AutoMapper;
using FastBus.Domain.Entities;
using FastBus.Domain.Entities.Identity;
using FastBus.Domain.Objects;
using FastBus.Services.Models.Car;
using FastBus.Services.Models.Driver;
using FastBus.Services.Models.Route;
using FastBus.Services.Models.User;
using FastBus.Web.Models.Car;
using FastBus.Web.Models.Driver;
using FastBus.Web.Models.Route;
using FastBus.Web.Models.User;

namespace FastBus.Web
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<ListItem, int>().ConvertUsing(cr => cr.Id);
            CreateMap<int, ListItem>().ConvertUsing(i => new ListItem { Id = i });

            DriverMap();
            UserMap();
            DispatcherMap();
            AccountMap();
            CarMap();
            RouteMap();
            ScheduleMap();
            BuyerMap();
        }

        private void UserMap()
        {
            CreateMap<User, EditUserViewModel>();
            CreateMap<EditUserViewModel, UserModel>()
                .ForMember(x => x.RegistredDate, map => map.Ignore())
                .ForMember(x => x.Role, map => map.Ignore());
            CreateMap<RegisterUserViewModelWithRole, User>()
                .ForMember(x => x.UserName, map => map.MapFrom(reg => reg.UserName))
                .ForMember(x => x.Email, map => map.MapFrom(reg => reg.Email))
                .ForMember(x => x.RegistredDate, map => map.MapFrom(mf => DateTime.Now))
                .ForMember(x => x.FirstName, map => map.MapFrom(reg => reg.FirstName))
                .ForMember(x => x.LastName, map => map.MapFrom(reg => reg.LastName))
                .ForMember(x => x.Patronymic, map => map.MapFrom(reg => reg.Patronymic))
                .ForMember(x => x.DateBorn, map => map.MapFrom(reg => reg.DateBorn))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<UserModel, UserViewModel>();
            CreateMap<QueryResult<UserModel>, UserResultViewModel>();
            CreateMap<UserSearchModel, UserSearchQuery>();
        }

        public void DispatcherMap()
        {

            CreateMap<RegisterUserViewModel, Dispatcher>()
                .ForMember(x => x.UserName, map => map.MapFrom(reg => reg.UserName))
                .ForMember(x => x.Email, map => map.MapFrom(reg => reg.Email))
                .ForMember(x => x.RegistredDate, map => map.MapFrom(mf => DateTime.Now))
                .ForMember(x => x.FirstName, map => map.MapFrom(reg => reg.FirstName))
                .ForMember(x => x.LastName, map => map.MapFrom(reg => reg.LastName))
                .ForMember(x => x.Patronymic, map => map.MapFrom(reg => reg.Patronymic))
                .ForMember(x => x.DateBorn, map => map.MapFrom(reg => reg.DateBorn))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void DriverMap()
        {
            CreateMap<RegisterUserViewModel, Driver>()
                .ForMember(x => x.UserName, map => map.MapFrom(reg => reg.UserName))
                .ForMember(x => x.Email, map => map.MapFrom(reg => reg.Email))
                .ForMember(x => x.RegistredDate, map => map.MapFrom(mf => DateTime.Now))
                .ForMember(x => x.FirstName, map => map.MapFrom(reg => reg.FirstName))
                .ForMember(x => x.LastName, map => map.MapFrom(reg => reg.LastName))
                .ForMember(x => x.Patronymic, map => map.MapFrom(reg => reg.Patronymic))
                .ForMember(x => x.DateBorn, map => map.MapFrom(reg => reg.DateBorn))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Driver, EditDriverViewModel>();
            CreateMap<EditDriverViewModel, DriverModel>()
                .ForMember(x => x.RegistredDate, map => map.Ignore());
            
            CreateMap<DriverModel, DriverViewModel>();
            CreateMap<QueryResult<DriverModel>, DriverResultViewModel>();
            CreateMap<DriverSearchModel, DriverSearchQuery>();
        }

        private void BuyerMap()
        {
            CreateMap<RegisterUserViewModel, Buyer>()
                .ForMember(x => x.UserName, map => map.MapFrom(reg => reg.UserName))
                .ForMember(x => x.Email, map => map.MapFrom(reg => reg.Email))
                .ForMember(x => x.RegistredDate, map => map.MapFrom(mf => DateTime.Now))
                .ForMember(x => x.FirstName, map => map.MapFrom(reg => reg.FirstName))
                .ForMember(x => x.LastName, map => map.MapFrom(reg => reg.LastName))
                .ForMember(x => x.Patronymic, map => map.MapFrom(reg => reg.Patronymic))
                .ForMember(x => x.DateBorn, map => map.MapFrom(reg => reg.DateBorn))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void AccountMap()
        {
            CreateMap<RegisterUserViewModel, User>()
                .ForMember(x => x.UserName, map => map.MapFrom(mf => mf.UserName))
                .ForMember(x => x.DateBorn, map => map.MapFrom(mf => mf.DateBorn))
                .ForMember(x => x.RegistredDate, map => map.MapFrom(mf => DateTime.Now))
                .ForMember(x => x.Email, map => map.MapFrom(mf => mf.Email))
                .ForMember(x => x.FirstName, map => map.MapFrom(mf => mf.FirstName))
                .ForMember(x => x.LastName, map => map.MapFrom(mf => mf.LastName))
                .ForMember(x => x.Patronymic, map => map.MapFrom(mf => mf.Patronymic))
                .ForAllOtherMembers(x => x.Ignore());
        }

        private void CarMap()
        {
            CreateMap<CarViewModel, CarModel>()
                .ForMember(x => x.Model, map => map.MapFrom(o => o.CarModel))
                .ReverseMap()
                .ForMember(x => x.CarModel, map => map.MapFrom(o => o.Model));
            CreateMap<AddCarViewModel, CarModelWithDrivers>()
                .ForMember(x => x.Model, map => map.MapFrom(o => o.CarModel))
                .ReverseMap()
                .ForMember(x => x.CarModel, map => map.MapFrom(o => o.Model));
            CreateMap<CarViewModelWithDrivers, CarModelWithDrivers>()
                .ForMember(x => x.Model, map => map.MapFrom(o => o.CarModel))
                .ReverseMap()
                .ForMember(x => x.CarModel, map => map.MapFrom(o => o.Model));
            CreateMap<QueryResult<CarModelWithDrivers>, CarResultViewModel>();
            CreateMap<CarSearchModel, CarSearchQuery>();

            CreateMap<Car, int>().ConvertUsing(cr => cr.Id);
            CreateMap<int, CarModel>().ConvertUsing(i => new CarModel { Id = i });
        }

        private void RouteMap()
        {
            CreateMap<RouteViewModel, RouteModel>().ReverseMap();
            CreateMap<QueryResult<RouteModel>, RouteResultViewModel>();
            CreateMap<RouteSearchModel, RouteSearchQuery>();
            
            CreateMap<TicketModelWithSchedule, TicketViewModel>()
                .ForMember(x => x.Number, opt => opt.MapFrom(map => map.Schedule.Number))
                .ForMember(x => x.Cost, opt => opt.MapFrom(map => map.Schedule.Cost))
                .ForMember(x => x.Departure, opt => opt.MapFrom(map => map.Schedule.Route.Departure))
                .ForMember(x => x.Destination, opt => opt.MapFrom(map => map.Schedule.Route.Destination))
                .ForMember(x => x.DepartureDate, opt => opt.MapFrom(map => map.Schedule.DepartureDate))
                .ForMember(x => x.DestinationDate, opt => opt.MapFrom(map => map.Schedule.DestinationDate));
            CreateMap<QueryResult<TicketModelWithSchedule>, TicketResultViewModel>();
            CreateMap<TicketSearchModel,TicketSearchQuery>();

            CreateMap<WayPointModel, WayPointViewModel>().ReverseMap();
        }

        private void ScheduleMap()
        {
            CreateMap<ScheduleModel, ScheduleViewModel>()
                .ForMember(x => x.PayTickets, map => map.MapFrom(x => x.Tickets.Count(t => t.IsPaid)))
                .ForMember(x => x.ReserveTickets, map => map.MapFrom(x => x.Tickets.Count(t => t.IsReserve)));

            CreateMap<BaseScheduleEditModel, ScheduleModel>()
                .ForMember(x => x.Tickets, map => map.Ignore())
                .ForMember(x => x.DepartureDate, map => map.Ignore())
                .ForMember(x => x.DestinationDate, map => map.Ignore())
                .ForMember(x => x.DispatcherName, map => map.Ignore())
                .ForMember(x => x.Car, map => map.Ignore())
                .ForMember(x => x.Route, map => map.Ignore());

            CreateMap<ScheduleEditModel, ScheduleModel>()
                .ForMember(x => x.Tickets, map => map.Ignore())
                .ForMember(x => x.DepartureDate, map => map.MapFrom(x =>
                    x.DepartureDate.AddHours(x.DepartureHours).AddMinutes(x.DepartureMinutes)))
                .ForMember(x => x.DestinationDate, map => map.MapFrom(x =>
                    x.DestinationDate.AddHours(x.DestinationHours).AddMinutes(x.DestinationMinutes)))
                .ForMember(x => x.DispatcherName, map => map.Ignore())
                .ForMember(x => x.Car, map => map.Ignore())
                .ForMember(x => x.Route, map => map.Ignore())
                .ReverseMap()
                .ForMember(x => x.DepartureHours, map => map.MapFrom(x => x.DepartureDate.Hour))
                .ForMember(x => x.DepartureMinutes, map => map.MapFrom(x => x.DepartureDate.Minute))
                .ForMember(x => x.DestinationHours, map => map.MapFrom(x => x.DestinationDate.Hour))
                .ForMember(x => x.DestinationMinutes, map => map.MapFrom(x => x.DestinationDate.Minute));
            CreateMap<QueryResult<ScheduleModel>, ScheduleResultViewModel>();
            CreateMap<ScheduleSearchModel, ScheduleSearchQuery>();
        }
    }
}