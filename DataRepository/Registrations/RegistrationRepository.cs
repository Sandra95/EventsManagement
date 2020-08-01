namespace DataRepository.Registrations
{
    using System.Threading.Tasks;
    using DataRepository.EFContext;
    using DataRepository.Models;

    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly EventsContext eventsContext;

        public RegistrationRepository(EventsContext eventsContext)
        {
            this.eventsContext = eventsContext;
        }

        public async Task<Registration> AddRegisterAsync(Registration registration)
        {
            try
            {
                await this.eventsContext.Registrations.AddAsync(registration);
                await this.eventsContext.SaveChangesAsync();

                return registration;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
    }
}
