namespace ApplicationServices.Registrations
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;

    public interface IRegistrationsService
    {
        Task<Guid> RegisteAsync(Guid eventId, RegistrationDto registration);
    }
}
