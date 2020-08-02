namespace Application.Services.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationServices.Adapters;
    using ApplicationServices.Events;
    using AutoFixture;
    using AutoMapper;
    using DataRepository.Events;
    using DataRepository.Models;
    using InfrastructureCrossCutting.Exceptions;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class EventsServiceTests
    {
        private EventsService target;
        private readonly Mock<IEventsRepository> eventsRepository;
        private IMapper mapper;
        private Fixture fixture;

        public EventsServiceTests()
        {
            this.mapper = this.SetupMapper();
            this.fixture = FixtureWithBehavior;
            this.eventsRepository = new Mock<IEventsRepository>();
            this.target = new EventsService(this.eventsRepository.Object, mapper);
        }

        [Fact]
        public async Task GetEventAsync_Id_EventDto()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var eventModel = this.fixture.Create<Event>();

            this.eventsRepository
                .Setup(i => i.GetEventAsync(It.IsAny<Guid>()))
                .ReturnsAsync(eventModel);

            //Act
            var result = await this.target.TryGetEventAsync(eventId);

            //Assert
            this.eventsRepository
                .Verify(i => i.GetEventAsync(eventId), Times.Once);
        }

        [Fact]
        public async Task GetEventAsync_IdDoesntExist_ThrowsNotFound()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            Event _event = null;

            this.eventsRepository
                .Setup(i => i.GetEventAsync(It.IsAny<Guid>()))
                .ReturnsAsync(_event);

            //Act && Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await this.target.TryGetEventAsync(eventId));
        }

        [Fact]
        public async Task CreateEventAsync_EventDto_EventId()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();

            this.eventsRepository
                .Setup(i => i.CreateEventAsync(It.IsAny<Event>()))
                .ReturnsAsync(eventId);

            //Act
            var result = await this.target.CreateEventAsync(eventDto);

            //Assert
            this.eventsRepository
                .Verify(i => i.CreateEventAsync(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task GetEventsAsync_NoParams_ListOfEvents()
        {
            //Arrange
            var events = this.fixture.CreateMany<Event>();

            this.eventsRepository
                .Setup(i => i.GetEventsAsync())
                .ReturnsAsync(events);

            //Act
            var result = await this.target.GetEventsAsync();

            //Assert
            this.eventsRepository
                .Verify(i => i.GetEventsAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateEventAsync_IdDoesntExist_ThrowNotFoundException()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();
            Event _event = null;

            this.eventsRepository
                .Setup(i => i.GetEventAsync(id))
                .ReturnsAsync(_event);

            //Act && Asset
            await Assert.ThrowsAsync<NotFoundException>(async () => await this.target.UpdateEventAsync(id, eventDto));
        }

        [Fact]
        public async Task UpdateEventAsync_Id_Event_CallsEventsRepository()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();
            var _event = this.fixture.Create<Event>();

            this.eventsRepository
                .Setup(i => i.GetEventAsync(id))
                .ReturnsAsync(_event);

            //Act
            await this.target.UpdateEventAsync(id, eventDto);

            //Asset
            this.eventsRepository
                .Verify(i => i.UpdateEventAsync(id, It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEventAsync_IdDoesntExist_ThrowsNotFoundException()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();
            Event _event = null;

            this.eventsRepository
                .Setup(i => i.GetEventAsync(id))
                .ReturnsAsync(_event);

            //Act && Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await this.target.DeleteEventAsync(id));
        }

        [Fact]
        public async Task DeleteEventAsync_Id_Event_CallsEventsRepository()
        {
            //Arrange
            var id = this.fixture.Create<Guid>();
            var eventDto = this.fixture.Create<EventDto>();
            var _event = this.fixture.Create<Event>();

            this.eventsRepository
                .Setup(i => i.GetEventAsync(id))
                .ReturnsAsync(_event);

            this.eventsRepository
                .Setup(i => i.DeleteEventAsync(id, _event))
                .Returns(Task.CompletedTask);

            //Act
            await this.target.DeleteEventAsync(id);

            //Asset
            this.eventsRepository
                .Verify(i => i.DeleteEventAsync(id, _event), Times.Once);

        }


        private IMapper SetupMapper()
        {
            var profiles = new Profile[]
        {
                new EventsProfile()
        };

            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));

            return new Mapper(configuration);
        }

        private Fixture FixtureWithBehavior
        {
            get
            {
                var fixture = new Fixture();
                fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                return fixture;
            }
        }
    }
}
