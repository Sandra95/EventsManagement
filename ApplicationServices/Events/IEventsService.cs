namespace ApplicationServices.Events
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationDTO;

    public interface IEventsService
    {
        Task<EventDto> TryGetEventAsync(Guid guid);

        Task<IEnumerable<EventDto>> GetEventsAsync();

        Task<IEnumerable<EventDto>> GetEventsAsync(string location);

        Task<Guid> CreateEventAsync(EventDto eventDto);

        Task UpdateEventAsync(Guid id, EventDto eventDto);
        Task DeleteEventAsync(Guid id);
    }
}
