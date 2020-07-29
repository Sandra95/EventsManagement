
namespace DataRepository.Events
{
    using System;
    using System.Threading.Tasks;
    using DataRepository.Models;

    public interface IEventsRepository
    {
        Task<Event> GetEventAsync(Guid id);

        Task<Guid> CreateEventAsync(Event _event);
    }
}
