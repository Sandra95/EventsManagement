namespace ApplicationServices.Adapters
{
    using ApplicationDTO;
    using AutoMapper;
    using DataRepository.Models;

    public class RegistrationProfile : Profile
    {

        public RegistrationProfile()
        {

            CreateMap<RegistrationDto, Registration>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(x => x.Age))
                .ForMember(dest => dest.NIF, opt => opt.MapFrom(x => x.NIF))
                .ReverseMap()
                .ForAllOtherMembers(dest => dest.Ignore());
        }
    }
}
