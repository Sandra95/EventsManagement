namespace ApplicationServices.Events
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;

    public interface IEventsService
    {
        Task<EventDto> GetEventAsync(Guid guid);

        Task<Guid> CreateEventAsync(EventDto eventDto);
    }
}
