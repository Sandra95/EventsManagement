namespace ApplicationServices.Registrations
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using AutoMapper;
    using DataRepository.Events;

    public class RegistrationsService : IRegistrationsService
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IRegistrationRepository registrationRepository;
        private readonly IMapper mapper;

        public RegistrationsService(IEventsRepository eventsRepository, IRegistrationRepository registrationRepository, IMapper)
        {
            this.eventsRepository = eventsRepository;
            this.registrationRepository = registrationRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> RegisteAsync(Guid eventId, RegistrationDto registration)
        {
            throw new NotImplementedException();
        }
    }
}
