namespace Presentation.Api.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationServices.Registrations;
    using AutoFixture;
    using EventsManagement.Controllers;
    using InfrastructureCrossCutting.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class RegistrationsControllerTests
    {
        private readonly Mock<IRegistrationsService> registrationsService;
        private RegistrationsController target;
        private Fixture fixture;

        public RegistrationsControllerTests()
        {
            this.fixture = new Fixture();
            this.registrationsService = new Mock<IRegistrationsService>();
            this.target = new RegistrationsController(this.registrationsService.Object);
        }

        [Fact]
        public async Task PostRegistration_EventId_Registration_Created()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registration = this.fixture.Create<RegistrationDto>();
            var registrationId = this.fixture.Create<Guid>();

            this.registrationsService
                .Setup(i => i.RegisterAsync(eventId, registration))
                .ReturnsAsync(registrationId);

            //Act
            var result = await this.target.PostRegistration(eventId, registration);
            var objectResult = (CreatedResult)result;

            //Assert
            Assert.Equal($"Registrations/{registrationId}", objectResult.Location);
            Assert.Equal((int)HttpStatusCode.Created, objectResult.StatusCode);
        }

        [Fact]
        public async Task PostRegistration_EventIdDoesntExist_Registration_NotFound()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registration = this.fixture.Create<RegistrationDto>();
            var registrationId = this.fixture.Create<Guid>();

            this.registrationsService
                .Setup(i => i.RegisterAsync(eventId, registration))
                .ThrowsAsync(new NotFoundException());

            //Act
            var result = await this.target.PostRegistration(eventId, registration);
            var objectResult = (NotFoundObjectResult)result;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task PostRegistration_EventSoldOut_BadRequest()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registration = this.fixture.Create<RegistrationDto>();
            var registrationId = this.fixture.Create<Guid>();

            this.registrationsService
                .Setup(i => i.RegisterAsync(eventId, registration))
                .ThrowsAsync(new EventSoldOutException());

            //Act
            var result = await this.target.PostRegistration(eventId, registration);
            var objectResult = (BadRequestObjectResult)result;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public async Task PostRegistration_EventId_RegistrationInvalid_BadRequest()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registration = this.fixture
                .Build<RegistrationDto>()
                .With(i => i.Name, string.Empty)
                .With(i => i.Age, 0)
                .With(i => i.NIF, 0)
                .Create();

            var registrationId = this.fixture.Create<Guid>();

            this.registrationsService
                .Setup(i => i.RegisterAsync(eventId, registration))
                .ThrowsAsync(new NotFoundException());

            //Act
            var result = await this.target.PostRegistration(eventId, registration);
            var objectResult = (BadRequestObjectResult)result;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteRegistration_EventId_RegistrationId_NoContent()
        {
            //Arrange
            var registrationId = this.fixture.Create<Guid>();

            this.registrationsService
                .Setup(i => i.DeleteRegisterAsync(registrationId))
                .Returns(Task.CompletedTask);

            //Act
            var result = await this.target.DeleteRegistration(registrationId);
            var objectResult = (NoContentResult)result;

            //Assert
            Assert.Equal((int)HttpStatusCode.NoContent, objectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteRegistration_EventIdDoesntHaveRegistrationId_NotFound()
        {
            //Arrange
            var registrationId = this.fixture.Create<Guid>();

            this.registrationsService
                .Setup(i => i.DeleteRegisterAsync(registrationId))
                .ThrowsAsync(new NotFoundException());

            //Act
            var result = await this.target.DeleteRegistration(registrationId);
            var objectResult = (NotFoundObjectResult)result;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetEventRegistrations_EventIdDoesntNotExist_NotFound()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();

            this.registrationsService
                .Setup(i => i.GetEventRegistrationsAsync(eventId))
                .ThrowsAsync(new NotFoundException());

            //Act
            var result = await this.target.GetEventRegistrations(eventId);
            var objectResult = (NotFoundObjectResult)result;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetEventRegistrations_EventId_Ok()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var events = this.fixture.CreateMany<RegistrationDto>();

            this.registrationsService
                .Setup(i => i.GetEventRegistrationsAsync(eventId))
                .ReturnsAsync(events);

            //Act
            var result = await this.target.GetEventRegistrations(eventId);
            var objectResult = (OkObjectResult)result;

            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }
    }
}
