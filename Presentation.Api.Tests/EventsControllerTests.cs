namespace Presentation.Api.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationServices.Events;
    using AutoFixture;
    using EventsManagement.Controllers;
    using InfrastructureCrossCutting.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class EventsControllerTests
    {
        private readonly Mock<IEventsService> eventsService;
        private Fixture fixture;
        private EventsController target;

        public EventsControllerTests()
        {
            this.fixture = new Fixture();
            this.eventsService = new Mock<IEventsService>();
            this.target = new EventsController(this.eventsService.Object);

        }

        [Fact]
        public async Task GetEvent_Id_Ok()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();

            this.eventsService
                .Setup(i => i.GetEventAsync(It.IsAny<Guid>()))
                .ReturnsAsync(eventDto);

            //Act
            var result = await this.target.GetEvent(eventId);
            var okObjectResult = result as OkObjectResult;


            //Assert
            this.eventsService.VerifyAll();
            Assert.True(okObjectResult.StatusCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetEvent_IdDoesntExist_ReturnsNotFound()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();

            this.eventsService
                .Setup(i => i.GetEventAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new NotFoundException("Event not found"));

            //Act
            var result = await this.target.GetEvent(eventId);
            var objectResult = result as NotFoundResult;


            //Assert
            this.eventsService.VerifyAll();
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.NotFound);
        }
    }
}
