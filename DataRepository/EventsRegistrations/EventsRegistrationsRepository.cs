using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Models;

namespace DataRepository.EventsRegistrations
{
    public class EventsRegistrationsRepository : IEventsRegistrationsRepository
    {
        public Task<IEnumerable<Attendee>> GetEventRegistrationsAsync(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> RegistAttendeeInEventAsync(Guid eventId, Guid attendeeId)
        {
            throw new NotImplementedException();
        }
    }
}
