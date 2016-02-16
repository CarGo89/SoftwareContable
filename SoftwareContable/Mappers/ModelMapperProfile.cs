using AutoMapper;
using SoftwareContable.Models;

namespace SoftwareContable.Mappers
{
    public class ModelMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Client, DataAccess.Entities.Client>();
            Mapper.CreateMap<DataAccess.Entities.Client, Client>();

            Mapper.CreateMap<DataAccess.Entities.Role, OptionCatalog>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));

            Mapper.CreateMap<Role, DataAccess.Entities.Role>();
            Mapper.CreateMap<DataAccess.Entities.Role, Role>();

            Mapper.CreateMap<User, DataAccess.Entities.User>()
                .ForMember(dest => dest.AuthorizedByUser, opt => opt.MapFrom(src => (User)null));

            Mapper.CreateMap<DataAccess.Entities.User, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => string.Empty));

            Mapper.CreateMap<DataAccess.Entities.SoldSystem, OptionCatalog>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));

            Mapper.CreateMap<SoldSystem, DataAccess.Entities.SoldSystem>();
            Mapper.CreateMap<DataAccess.Entities.SoldSystem, SoldSystem>();

            Mapper.CreateMap<Report, DataAccess.Entities.Report>()
                .ForMember(dest => dest.Client, opt => opt.MapFrom(src => (Client)null))
                .ForMember(dest => dest.SoldSystem, opt => opt.MapFrom(src => (SoldSystem)null));

            Mapper.CreateMap<DataAccess.Entities.Report, Report>();
        }
    }
}