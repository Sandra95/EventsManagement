using System;
using System.Linq;
using System.Threading.Tasks;
using DataRepository.EFContext;
using DataRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Events
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventsContext eventsContext;

        public EventsRepository(EventsContext eventsContext)
        {
            this.eventsContext = eventsContext;
        }

        public async Task<Guid> CreateEventAsync(Event _event)
        {
            await this.eventsContext.AddAsync(_event);
            await this.eventsContext.SaveChangesAsync();
            //TODO: Do not return bd id
            return _event.Id;
        }

        public async Task<Event> GetEventAsync(Guid id)
        {
            return await this.eventsContext
                .Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
