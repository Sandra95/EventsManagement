namespace EventsManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationDTO.Validator;
    using ApplicationServices.Events;
    using InfrastructureCrossCutting.Exceptions;
    using InfrastructureCrossCutting.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;

        public EventsController(IEventsService eventsService)
        {
            this.eventsService = eventsService;
        }

        /// <summary>
		/// Gets Event.
		/// </summary>
		/// <param name="id">The identifier of the event.</param>
		/// <returns></returns>
		[HttpGet("{id}", Name = nameof(GetEvent))]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            try
            {
                var eventDto = await this.eventsService.TryGetEventAsync(id);
                return this.Ok(eventDto);
            }
            catch (NotFoundException ex)
            {
                //TODO: Log Error
                return this.NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets all created events.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <returns></returns>
        [HttpGet("", Name = nameof(GetEvents))]
        [ProducesResponseType(typeof(IEnumerable<EventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvents()
        {
            var events = await this.eventsService.GetEventsAsync();
            return this.Ok(events);
        }

        /// <summary>
        /// Gets all created events.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <returns></returns>
        [HttpGet("Location", Name = nameof(GetEventsByLocation))]
        [ProducesResponseType(typeof(IEnumerable<EventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventsByLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return this.BadRequest("Please indicate a region");
            }
            var events = await this.eventsService.GetEventsAsync(location);
            return this.Ok(events);
        }

        /// <summary>
        /// Create new event.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <returns></returns>
        [HttpPost("", Name = nameof(PostEvent))]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostEvent([FromBody] EventDto eventDto)
        {
            if (!ModelState.IsValid || eventDto == null)
            {
                return this.BadRequest($"The following parameters have invalid values: {this.ModelState.GetAllInvalidKeys()}.");
            }

            var validator = new EventDtoValidator();

            if (validator.Validate(eventDto))
            {
                var eventId = await this.eventsService.CreateEventAsync(eventDto);
                return this.Created($"events/{eventId}", eventId);
            }

            return this.BadRequest(validator.GetErrors());

        }

        /// <summary>
        /// Updates an exsting event.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateEvent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] EventDto eventDto)
        {
            try
            {
                if (!ModelState.IsValid || eventDto == null)
                {
                    return this.BadRequest($"The following parameters have invalid values: {this.ModelState.GetAllInvalidKeys()}.");
                }

                var validator = new EventDtoValidator();

                if (validator.Validate(eventDto))
                {
                    await this.eventsService.UpdateEventAsync(id, eventDto);
                    return this.NoContent();
                }

                return this.BadRequest(validator.GetErrors());

            }
            catch (NotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Deletes an exsting event.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteEvent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            try
            {

                await this.eventsService.DeleteEventAsync(id);
                return this.NoContent();
            }

            catch (NotFoundException ex)
            {
                return this.NotFound(ex.Message);
            }
        }
    }
}
