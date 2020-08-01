namespace ApplicationServices.Events
{
    using System;
    using System.Collections.Generic;
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


        public async Task<EventDto> TryGetEventAsync(Guid eventId)
        {
            try
            {
                var _event = await this.GetEventAsync(eventId);
                return this.mapper.Map<EventDto>(_event);

            }
            catch (NotFoundException ex)
            {

                throw;
            }
        }

        public async Task<Guid> CreateEventAsync(EventDto eventDto)
        {
            var eventModel = this.mapper.Map<Event>(eventDto);
            return await this.eventsRespository.CreateEventAsync(eventModel);
        }

        public async Task<IEnumerable<EventDto>> GetEventsAsync()
        {
            var events = await this.eventsRespository.GetEventsAsync();

            return (this.mapper.Map<IEnumerable<EventDto>>(events));
        }

        public async Task UpdateEventAsync(Guid id, EventDto eventDto)
        {
            try
            {
                var _event = await this.GetEventAsync(id);

                _event.Description = eventDto.Description;
                _event.Name = eventDto.Name;
                _event.Location = eventDto.Location;
                _event.MaxAttendance = eventDto.MaxAttendance;
                _event.DueDate = eventDto.DueDate;

                await this.eventsRespository.UpdateEventAsync(id, _event);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
        }

        public async Task DeleteEventAsync(Guid id)
        {
            try
            {
                var _event = await this.GetEventAsync(id);

                await this.eventsRespository.DeleteEventAsync(id, _event);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
        }
        private async Task<Event> GetEventAsync(Guid eventId)
        {
            var _event = await this.eventsRespository.GetEventAsync(eventId);

            if (_event == null)
            {
                throw new NotFoundException($"Could not found event with {nameof(eventId)}={eventId}.");
            }

            return _event;
        }

    }
}
