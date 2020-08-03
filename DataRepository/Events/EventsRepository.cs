using System;
using System.Collections.Generic;
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
            try
            {
                await this.eventsContext.AddAsync(_event);
                await this.eventsContext.SaveChangesAsync();
                //TODO: Do not return bd id
                return _event.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task DeleteEventAsync(Guid id, Event _event)
        {
            try
            {
                this.eventsContext.Events.Remove(_event);
                await this.eventsContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Event> GetEventAsync(Guid id)
        {
            try
            {
                return await this.eventsContext
                      .Events
                      .Where(e => e.Id == id)
                      .AsNoTracking()
                      .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            try
            {
                return await this.eventsContext
               .Events
               .AsNoTracking()
               .ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<Event>> GetEventsAsync(string location)
        {
            return await this.eventsContext
                .Events
                .Where(i => i.Location.Equals(location))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateEventAsync(Guid id, Event eventModel)
        {
            try
            {
                this.eventsContext.Entry(eventModel).State = EntityState.Modified;

                await this.eventsContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
