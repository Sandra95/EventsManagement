namespace DataRepository.Registrations
{
    using System.Threading.Tasks;
    using DataRepository.Models;

    public interface IRegistrationRepository
    {

        Task<Registration> AddRegisterAsync(Registration registrationDto);
    }
}
