namespace EventsManagement.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationDTO.Validator;
    using ApplicationServices.Registrations;
    using InfrastructureCrossCutting.Exceptions;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostRegistration(Guid eventId, [FromBody] RegistrationDto registration)
        {
            try
            {
                var registrationValidator = new RegistrationValidator();

                if (registrationValidator.Validate(registration))
                {
                    var resgistrationId = await this.registrationsService.RegisteAsync(eventId, registration);
                    return this.CreatedAtRoute($"Registrations/{resgistrationId}", resgistrationId);
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
    }
}
