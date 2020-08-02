namespace ApplicationServices.Registrations
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;

    public interface IRegistrationsService
    {
        Task<Guid> RegisterAsync(Guid eventId, RegistrationDto registration);

        Task DeleteRegisterAsync(Guid eventId, Guid registrationId);
    }
}
