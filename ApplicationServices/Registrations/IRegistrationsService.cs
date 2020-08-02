namespace ApplicationServices.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationDTO;

    public interface IRegistrationsService
    {
        Task<Guid> RegisterAsync(Guid eventId, RegistrationDto registration);

        Task DeleteRegisterAsync(Guid eventRegistrationId);

        Task<IEnumerable<RegistrationDto>> GetEventRegistrationsAsync(Guid eventId);
    }
}
