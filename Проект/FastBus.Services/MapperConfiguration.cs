using AutoMapper;
using FastBus.DAL.Concrete.Entities;
using FastBus.DAL.Concrete.Entities.Identity;
using FastBus.Services.Models.Car;
using FastBus.Services.Models.User;

namespace FastBus.Services
{
    public class ServiceMappingProfile : Profile
    {
        protected override void Configure()
        {
            #region UserMap

            CreateMap<User, UserModel>()
                .ForMember(um => um.Role, map => map.Ignore())
                .ReverseMap();

            #endregion

            #region CarMap

            CreateMap<Car, CarModel>().ReverseMap();
            CreateMap<DriverCarItem, User>()
                .ForMember(u => u.Name, map => map.MapFrom(c => c.Name))
                .ForMember(u => u.Id, map => map.MapFrom(c => c.Id))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<User, DriverCarItem>();

            #endregion
        }
    }
}
