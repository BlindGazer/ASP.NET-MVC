using AutoMapper;
using FastBus.Domain.Entities;
using FastBus.Domain.Entities.Identity;
using FastBus.Domain.Objects;
using FastBus.Services.Models.Car;
using FastBus.Services.Models.Driver;
using FastBus.Services.Models.Route;
using FastBus.Services.Models.User;

namespace FastBus.Services
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            UserMap();
            CarMap();
            DriverMap();
            RouteMap();
        }

        private void UserMap()
        {
            CreateMap<User, UserModel>()
                .ForMember(um => um.Role, map => map.Ignore())
                .ReverseMap();
        }

        private void CarMap()
        {
            CreateMap<Car, CarModel>().ReverseMap();
            CreateMap<Car, CarModelWithDrivers>().ReverseMap();
        }

        private void DriverMap()
        {
            CreateMap<Driver, ListItem>()
                .ForMember(c => c.Name, map => map.MapFrom(d => d.FirstName + " " + d.LastName + " " + d.Patronymic))
                .ReverseMap()
                .ForMember(u => u.Id, map => map.MapFrom(c => c.Id))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<Driver, DriverModel>().ReverseMap();
        }

        private void RouteMap()
        {
            CreateMap<Route, RouteModel>()
                .ReverseMap()
                .ForMember(x => x.Schedule, map => map.Ignore());

            CreateMap<RouteWayPoint, WayPointModel>()
                .ReverseMap()
                .ForMember( x => x.Route, map => map.Ignore());

            CreateMap<WayPoint, ListItem>()
                .ReverseMap()
                .ForMember(x => x.Routes, map => map.Ignore());

            CreateMap<Ticket, TicketModel>();
            CreateMap<Ticket, TicketModelWithSchedule>()
                .ForMember(x => x.Schedule, opt => opt.MapFrom(map => map.ScheduleItem));

            CreateMap<ScheduleItem, ScheduleModel>()
                .ForMember(x => x.DispatcherName, map => map.MapFrom(x => 
                    x.Dispatcher.FirstName + " " + x.Dispatcher.LastName + " " + x.Dispatcher.Patronymic))
                .ForMember(x => x.Seats, map => map.MapFrom(x => x.Seats < 1 ? x.Car.Seats : x.Seats))
                .ReverseMap()
                .ForMember(x => x.Tickets, map => map.Ignore())
                .ForMember(x => x.Route, map => map.Ignore())
                .ForMember(x => x.Car, map => map.Ignore())
                .ForMember(x => x.Drivers, map => map.Ignore())
                .ForMember(x => x.Dispatcher, map => map.Ignore());
        }
    }
}
