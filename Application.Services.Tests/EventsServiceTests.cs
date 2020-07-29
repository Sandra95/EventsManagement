namespace Application.Services.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
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
            this.fixture = new Fixture();
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
            var result = await this.target.GetEventAsync(eventId);

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
            await Assert.ThrowsAsync<NotFoundException>(async () => await this.target.GetEventAsync(eventId));
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
    }
}
