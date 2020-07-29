namespace ApplicationServices.Events
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using AutoMapper;
    using DataRepository.Events;
    using DataRepository.Models;
    using InfrastructureCrossCutting.Exceptions;

    public class EventsService : IEventsService
    {
        private readonly IEventsRepository eventsRespository;
        private readonly IMapper mapper;

        public EventsService(IEventsRepository eventsRespository, IMapper mapper)
        {
            this.eventsRespository = eventsRespository;
            this.mapper = mapper;
        }

        public async Task<EventDto> GetEventAsync(Guid eventId)
        {
            var _event = await this.eventsRespository.GetEventAsync(eventId);

            if (_event == null)
            {
                throw new NotFoundException($"Could not found event with {nameof(eventId)}={eventId}.");
            }

            return this.mapper.Map<EventDto>(_event);
        }

        public async Task<Guid> CreateEventAsync(EventDto eventDto)
        {
            var eventModel = this.mapper.Map<Event>(eventDto);
            return await this.eventsRespository.CreateEventAsync(eventModel);
        }
    }
}
