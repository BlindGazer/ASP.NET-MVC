using System;
using AutoMapper;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.DAL.Objects;
using FastBus.Services.Models.Car;
using FastBus.Services.Models.User;
using FastBus.Web.Models;
using FastBus.Web.Models.Car;
using FastBus.Web.Models.User;

namespace FastBus.Web
{
    public class WebMappingProfile : Profile
    {
        protected override void Configure()
        {
            UserMap();
            AccountMap();
            CarMap();
        }

        private void UserMap()
        {
            CreateMap<UserModel, UserViewModel>();
            CreateMap<QueryResult<UserModel>, QueryResult<UserViewModel>>();
            CreateMap<UserSearchModel, UserSearchQuery>();
        }
        private void AccountMap()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.UserName, map => map.MapFrom(mf => mf.UserName))
                .ForMember(x => x.DateBorn, map => map.MapFrom(mf => mf.DateBorn))
                .ForMember(x => x.RegistredDate, map => map.MapFrom(mf => DateTime.Now))
                .ForMember(x => x.Email, map => map.MapFrom(mf => mf.Email))
                .ForMember(x => x.Name, map => map.MapFrom(mf => mf.Name))
                .ForAllOtherMembers(x => x.Ignore());
        }
        private void CarMap()
        {
            CreateMap<CarViewModel, CarModel>().ReverseMap();
            CreateMap<DriverCarViewItem, DriverCarItem>().ReverseMap();

            CreateMap<DriverCarItem, int>().ConvertUsing(cr => cr.Id);
            CreateMap<int, DriverCarItem>().ConvertUsing(i => new DriverCarItem { Id = i });

            CreateMap<AddCarViewModel, CarModel>()
                .ForMember(cm => cm.Status, map => map.Ignore())
                .ForMember(cm => cm.Model, map => map.MapFrom(ac => ac.CarModel))
                .ReverseMap()
                .ForMember(ac => ac.CarModel, map => map.MapFrom(cm => cm.Model));
            CreateMap<QueryResult<CarModel>, QueryResult<CarViewModel>>();
            CreateMap<CarSearchModel, CarSearchQuery>();
        }
    }
}