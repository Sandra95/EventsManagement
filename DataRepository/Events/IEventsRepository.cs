
namespace DataRepository.Events
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataRepository.Models;

    public interface IEventsRepository
    {
        Task<Event> GetEventAsync(Guid id);

        Task<IEnumerable<Event>> GetEventsAsync(string location);

        Task<Guid> CreateEventAsync(Event _event);

        Task<IEnumerable<Event>> GetEventsAsync();

        Task UpdateEventAsync(Guid id, Event eventModel);

        Task DeleteEventAsync(Guid id, Event eventModel);

    }
}
