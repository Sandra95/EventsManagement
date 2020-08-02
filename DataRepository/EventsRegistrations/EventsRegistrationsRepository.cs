namespace DataRepository.EventsRegistrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataRepository.EFContext;
    using DataRepository.Models;
    using Microsoft.EntityFrameworkCore;

    public class EventsRegistrationsRepository : IEventsRegistrationsRepository
    {
        private readonly EventsContext eventContext;

        public EventsRegistrationsRepository(EventsContext eventContext)
        {
            this.eventContext = eventContext;
        }

        public async Task<int> CountEventRegistrationsAsync(Guid eventId)
        {
            try
            {
                return await this.eventContext
                   .EventsRegistrations
                   .Where(i => i.EventId == eventId)
                   .AsNoTracking()
                   .CountAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<Registration>> GetEventRegistrationsAsync(Guid eventId)
        {
            try
            {
                return await this.eventContext
                  .EventsRegistrations
                  .Include(i => i.Registration)
                  .Where(i => i.EventId == eventId)
                  .Select(i => i.Registration)
                  .AsNoTracking()
                  .ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Guid> AddRegisterToEventAsync(EventRegistration eventRegistration)
        {
            try
            {
                await this.eventContext.EventsRegistrations.AddAsync(eventRegistration);
                await this.eventContext.SaveChangesAsync();
                return eventRegistration.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task DeleteEventRegistrationAsync(EventRegistration eventRegistration)
        {
            try
            {
                this.eventContext.Remove(eventRegistration);
                await this.eventContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<EventRegistration> GetEventRegistrationAsync(Guid eventRegistrationId)
        {
            try
            {
                return await this.eventContext
                    .EventsRegistrations
                    .Where(i => i.Id == eventRegistrationId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
