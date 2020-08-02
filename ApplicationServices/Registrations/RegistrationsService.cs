namespace ApplicationServices.Registrations
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using AutoMapper;
    using DataRepository.Events;
    using DataRepository.EventsRegistrations;
    using DataRepository.Models;
    using DataRepository.Registrations;
    using InfrastructureCrossCutting.Exceptions;

    public class RegistrationsService : IRegistrationsService
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IRegistrationRepository registrationRepository;
        private readonly IEventsRegistrationsRepository eventsRegistrationsRepository;
        private readonly IMapper mapper;

        public RegistrationsService(
            IEventsRepository eventsRepository,
            IRegistrationRepository registrationRepository,
            IEventsRegistrationsRepository eventsRegistrationsRepository,
            IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.registrationRepository = registrationRepository;
            this.eventsRegistrationsRepository = eventsRegistrationsRepository;
            this.mapper = mapper;
        }

        public Task DeleteRegisterAsync(Guid eventId, Guid registrationId)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> RegisterAsync(Guid eventId, RegistrationDto registration)
        {
            var _event = await this.VerifyIfEventExistsAsync(eventId);

            await this.VerifyIfEventIsSoldOutAsync(_event);

            var resgistrationModel = await AddRegisterAsync(registration);

            var eventRegistrationId = await AddRegisterToEventAsync(_event, resgistrationModel);

            return eventRegistrationId;
        }

        private async Task<Guid> AddRegisterToEventAsync(Event _event, Registration resgistrationModel)
        {
            var eventRegistry = new EventRegistration
            {
                EventId = _event.Id,
                RegistrationId = resgistrationModel.Id
            };

            var eventRegistrationId = await this.eventsRegistrationsRepository.AddRegisterToEventAsync(eventRegistry);
            return eventRegistrationId;
        }

        private async Task<Registration> AddRegisterAsync(RegistrationDto registration)
        {
            var resgistrationModel = this.mapper.Map<Registration>(registration);

            resgistrationModel = await this.registrationRepository.AddRegisterAsync(resgistrationModel);
            return resgistrationModel;
        }

        private async Task<Event> VerifyIfEventExistsAsync(Guid eventId)
        {
            var _event = await this.eventsRepository.GetEventAsync(eventId);

            if (_event == null)
            {
                throw new NotFoundException("The event that you're trying to register does not exist!");
            }

            return _event;
        }

        private async Task VerifyIfEventIsSoldOutAsync(Event _event)
        {
            var nrRegistrations = await this.eventsRegistrationsRepository.CountEventRegistrationsAsync(_event.Id);

            if (nrRegistrations == _event.MaxAttendance)
            {
                throw new EventSoldOutException("This event is sold out!");
            }
        }
    }
}
