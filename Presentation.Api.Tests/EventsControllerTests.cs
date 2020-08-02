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
                .Setup(i => i.TryGetEventAsync(It.IsAny<Guid>()))
                .ReturnsAsync(eventDto);

            //Act
            var result = await this.target.GetEvent(eventId);
            var okObjectResult = result as OkObjectResult;


            //Assert
            this.eventsService.VerifyAll();
            Assert.True(okObjectResult.StatusCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetEvent_IdDoesntExist_NotFound()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();

            this.eventsService
                .Setup(i => i.TryGetEventAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new NotFoundException());

            //Act
            var result = await this.target.GetEvent(eventId);
            var notFoundResult = result as NotFoundObjectResult;


            //Assert
            this.eventsService.VerifyAll();
            Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);
        }


        [Fact]
        public async Task GetEvents_NoParams_Ok()
        {
            //Arrange
            var events = this.fixture.CreateMany<EventDto>();

            this.eventsService
                .Setup(i => i.GetEventsAsync())
                .ReturnsAsync(events);

            //Act
            var result = await this.target.GetEvents();
            var okObjectResult = result as OkObjectResult;


            //Assert
            this.eventsService.VerifyAll();
            Assert.True(okObjectResult.StatusCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostEvent_Event_Created()
        {
            //Arrange
            var eventDto = this.fixture
                .Build<EventDto>()
                .With(i => i.Description, "Desc")
                .With(i => i.Name, "name")
                .With(i => i.Location, "Braga")
                .With(i => i.DueDate, DateTime.Now.AddDays(1))
                .Create();

            var eventId = this.fixture.Create<Guid>();

            this.eventsService
                .Setup(i => i.CreateEventAsync(It.IsAny<EventDto>()))
                .ReturnsAsync(eventId);

            //Act
            var result = await this.target.PostEvent(eventDto);
            var objectResult = result as CreatedResult;


            //Assert
            this.eventsService.VerifyAll();
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.Created);
            Assert.Equal($"events/{eventId}", objectResult.Location);
        }

        [Fact]
        public async Task PostEvent_EventInvalidData_BadRequest()
        {
            //Arrange
            var eventDto = this.fixture
                .Build<EventDto>()
                .With(i => i.DueDate, DateTime.Now)
                .With(i => i.Location, string.Empty)
                .With(i => i.Name, string.Empty)
                .Create();

            //Act
            var result = await this.target.PostEvent(eventDto);
            var objectResult = result as BadRequestObjectResult;


            //Assert
            this.eventsService.Verify(i => i.CreateEventAsync(It.IsAny<EventDto>()), Times.Never);
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateEvent_EventInvalidData_BadRequest()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();
            var eventDto = this.fixture
                .Build<EventDto>()
                .With(i => i.DueDate, DateTime.Now)
                .Create();

            //Act
            var result = await this.target.UpdateEvent(id, eventDto);
            var objectResult = result as BadRequestObjectResult;


            //Assert
            this.eventsService.Verify(i => i.UpdateEventAsync(id, eventDto), Times.Never);
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateEvent_Id_ValidEventDto_NoContent()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();

            var eventDto = this.fixture
                .Build<EventDto>()
                .With(x => x.DueDate, DateTime.Today.AddDays(1))
                .Create();

            this.eventsService
                .Setup(i => i.UpdateEventAsync(It.IsAny<Guid>(), It.IsAny<EventDto>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await this.target.UpdateEvent(id, eventDto);
            var objectResult = result as NoContentResult;


            //Assert
            this.eventsService.Verify(i => i.UpdateEventAsync(id, eventDto), Times.Once);
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task UpdateEvent_Id_NotFound()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();
            var eventDto = this.fixture
                .Build<EventDto>()
                .With(i => i.Description, "Desc")
                .With(i => i.Name, "name")
                .With(i => i.Location, "Braga")
                .With(i => i.DueDate, DateTime.Now.AddDays(1))
                .Create();

            this.eventsService
                .Setup(i => i.UpdateEventAsync(id, eventDto))
                .ThrowsAsync(new NotFoundException());

            //Act
            var result = await this.target.UpdateEvent(id, eventDto);
            var objectResult = result as NotFoundObjectResult;


            //Assert
            this.eventsService.Verify(i => i.UpdateEventAsync(id, eventDto), Times.Once);
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteEvent_Id_NotFound()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();

            this.eventsService
                .Setup(i => i.DeleteEventAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new NotFoundException());

            //Act
            var result = await this.target.DeleteEvent(id);
            var objectResult = result as NotFoundObjectResult;


            //Assert
            this.eventsService.Verify(i => i.DeleteEventAsync(id), Times.Once);
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteEvent_Id_NoContent()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();

            this.eventsService
                .Setup(i => i.DeleteEventAsync(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            //Act
            var result = await this.target.DeleteEvent(id);
            var objectResult = result as NoContentResult;


            //Assert
            this.eventsService.Verify(i => i.DeleteEventAsync(id), Times.Once);
            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.NoContent);
        }
    }
}
