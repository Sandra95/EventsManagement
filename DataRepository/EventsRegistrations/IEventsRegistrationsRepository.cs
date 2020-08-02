namespace DataRepository.EventsRegistrations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataRepository.Models;

    public interface IEventsRegistrationsRepository
    {
        Task<Guid> AddRegisterToEventAsync(EventRegistration eventRegistration);

        Task<IEnumerable<Registration>> GetEventRegistrationsAsync(Guid eventRegistrationId);

        Task<int> CountEventRegistrationsAsync(Guid eventId);

        Task DeleteEventRegistrationAsync(EventRegistration eventRegistration);

        Task<EventRegistration> GetEventRegistrationAsync(Guid eventRegistrationId);
    }
}
