namespace DataRepository.Registrations
{
    using System;
    using System.Threading.Tasks;
    public interface IRegistrationRepository
    {

        Task<Guid> RegisterAttendeeAsync();
    }
}
