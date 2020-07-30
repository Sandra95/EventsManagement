namespace EventsManagement.Controllers
{
    using System;
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
		[HttpGet("", Name = nameof(GetEvent))]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            try
            {
                var eventDto = await this.eventsService.GetEventAsync(id);
                return this.Ok(eventDto);
            }
            catch (NotFoundException ex)
            {
                //TODO: Log Error
                return this.NotFound();
            }
        }

        /// <summary>
        /// Create new event.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <returns></returns>
        [HttpPost("", Name = nameof(PostEvent))]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostEvent(EventDto eventDto)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest($"The following parameters have invalid values: {this.ModelState.GetAllInvalidKeys()}.");
            }

            var validator = new EventDtoValidator();

            if (validator.Valid(eventDto))
            {
                var eventId = await this.eventsService.CreateEventAsync(eventDto);
                return this.Created($"events/{eventId}", eventId);
            }

            return this.BadRequest(validator.GetErrors());

        }
    }
}
