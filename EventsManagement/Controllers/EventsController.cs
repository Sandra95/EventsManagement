namespace EventsManagement.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationServices.Events;
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
		/// Gets boxes.
		/// </summary>
		/// <param name="id">The identifier of the event.</param>
		/// <returns></returns>
		[HttpGet("", Name = nameof(GetEvent))]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var eventDto = await this.eventsService.GetEventAsync(id);
            return Ok(eventDto);
        }

        /// <summary>
        /// Gets boxes.
        /// </summary>
        /// <param name="id">The identifier of the event.</param>
        /// <returns></returns>
        [HttpPost("", Name = nameof(PostEvent))]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostEvent(EventDto eventDto)
        {
            var eventId = await this.eventsService.CreateEventAsync(eventDto);
            return this.Created($"events/{eventId}", eventId);
        }
    }
}
