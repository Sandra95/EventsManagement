namespace DataRepository.EventsRegistrations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataRepository.Models;

    public interface IEventsRegistrationsRepository
    {
        Task<Guid> RegistAttendeeInEventAsync(Guid eventId, Guid attendeeId);

        Task<IEnumerable<Attendee>> GetEventRegistrationsAsync(Guid eventId);
    }
}
