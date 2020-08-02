namespace EventsManagement.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationDTO.Validator;
    using ApplicationServices.Registrations;
    using InfrastructureCrossCutting.Exceptions;
    using InfrastructureCrossCutting.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("/[controller]")]
    public class RegistrationsController : Controller
    {
        private readonly IRegistrationsService registrationsService;

        public RegistrationsController(IRegistrationsService registrationsService)
        {
            this.registrationsService = registrationsService;
        }

        [HttpPost("", Name = nameof(PostRegistration))]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostRegistration(Guid eventId, [FromBody] RegistrationDto registration)
        {
            try
            {
                if (!ModelState.IsValid || registration == null)
                {
                    return this.BadRequest($"The following parameters have invalid values: {this.ModelState.GetAllInvalidKeys()}.");
                }

                var registrationValidator = new RegistrationValidator();

                if (registrationValidator.Validate(registration))
                {
                    var resgistrationId = await this.registrationsService.RegisterAsync(eventId, registration);
                    return this.Created($"Registrations/{resgistrationId}", resgistrationId);
                }

                return this.BadRequest(registrationValidator.GetErrors());


            }
            catch (NotFoundException ex)
            {

                return this.NotFound(ex.Message);
            }

            catch (EventSoldOutException ex)
            {
                return this.BadRequest(ex.Message);
            }

        }


        [HttpDelete("{eventRegistrationId}", Name = nameof(DeleteRegistration))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRegistration(Guid eventRegistrationId)
        {
            try
            {
                await this.registrationsService.DeleteRegisterAsync(eventRegistrationId);
                return this.NoContent();
            }
            catch (NotFoundException ex)
            {

                return this.NotFound(ex.Message);
            }
        }


        [HttpGet("{eventId}", Name = nameof(GetEventRegistrations))]
        [ProducesResponseType((typeof(IEnumerable<RegistrationDto>)), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventRegistrations(Guid eventId)
        {
            try
            {
                var registrations = await this.registrationsService.GetEventRegistrationsAsync(eventId);
                return this.Ok(registrations);
            }
            catch (NotFoundException ex)
            {

                return this.NotFound(ex.Message);
            }
        }
    }
}
