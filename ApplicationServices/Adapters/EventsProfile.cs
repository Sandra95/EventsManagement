namespace ApplicationServices.Adapters
{
    using ApplicationDTO;
    using AutoMapper;
    using DataRepository.Models;

    public class EventsProfile : Profile
    {

        public EventsProfile()
        {
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(x => x.Location))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(x => x.DueDate))
                .ForMember(dest => dest.MaxAttendance, opt => opt.MapFrom(x => x.MaxAttendance))
                .ReverseMap()
                .ForAllOtherMembers(dest => dest.Ignore());


        }
    }
}
