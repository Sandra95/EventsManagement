namespace Application.Services.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationServices.Adapters;
    using ApplicationServices.Registrations;
    using AutoFixture;
    using AutoMapper;
    using DataRepository.Events;
    using DataRepository.EventsRegistrations;
    using DataRepository.Models;
    using DataRepository.Registrations;
    using InfrastructureCrossCutting.Exceptions;
    using Moq;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class RegistrationsServiceTests
    {
        private RegistrationsService target;
        private Mock<IEventsRepository> eventsRepository;
        private Mock<IRegistrationRepository> registrationRepository;
        private Mock<IEventsRegistrationsRepository> eventsRegistrationsRepository;
        private Fixture fixture;
        private IMapper mapper;

        public RegistrationsServiceTests()
        {
            this.mapper = this.SetupMapper();
            this.fixture = FixtureWithBehavior;
            this.eventsRepository = new Mock<IEventsRepository>();
            this.registrationRepository = new Mock<IRegistrationRepository>();
            this.eventsRegistrationsRepository = new Mock<IEventsRegistrationsRepository>();

            this.target = new RegistrationsService(
                this.eventsRepository.Object,
                this.registrationRepository.Object,
                this.eventsRegistrationsRepository.Object,
                this.mapper);
        }

        [Fact]
        public async Task RegisteAsync_Id_Registration_RegistrationId()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var maxAttendance = this.fixture.Create<int>();
            var registrationDto = this.fixture.Create<RegistrationDto>();
            var registration = this.fixture.Create<Registration>();
            var registrationId = this.fixture.Create<Guid>();
            var eventRegistrationId = this.fixture.Create<Guid>();

            var _event = this.fixture
                .Build<Event>()
                .With(i => i.MaxAttendance, maxAttendance)
                .Create();

            this.eventsRepository
                .Setup(i => i.GetEventAsync(eventId))
                .ReturnsAsync(_event);

            this.eventsRegistrationsRepository
                .Setup(i => i.CountEventRegistrationsAsync(_event.Id))
                .ReturnsAsync(maxAttendance - 1);

            this.registrationRepository
                .Setup(i => i.AddRegisterAsync(It.IsAny<Registration>()))
                .ReturnsAsync(registration);

            this.eventsRegistrationsRepository
                .Setup(i => i.AddRegisterToEventAsync(It.IsAny<EventRegistration>()))
                .ReturnsAsync(eventRegistrationId);


            //Act
            var result = await this.target.RegisterAsync(eventId, registrationDto);

            //Assert
            this.eventsRepository.VerifyAll();
            this.registrationRepository.VerifyAll();
            this.eventsRegistrationsRepository.VerifyAll();
        }

        [Fact]
        public async Task RegisteAsync_IdDoesntExist_Registration_ThrowsNotFoundException()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registrationDto = this.fixture.Create<RegistrationDto>();

            Event _event = null;

            this.eventsRepository
                .Setup(i => i.GetEventAsync(eventId))
                .ReturnsAsync(_event);


            //Act && Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await this.target.RegisterAsync(eventId, registrationDto));
        }

        [Fact]
        public async Task RegisteAsync_EventSoldOut_ThrowsEventSoldOutException()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var maxAttendance = this.fixture.Create<int>();
            var registrationDto = this.fixture.Create<RegistrationDto>();

            var _event = this.fixture
                .Build<Event>()
                .With(i => i.MaxAttendance, maxAttendance)
                .Create();

            this.eventsRepository
                .Setup(i => i.GetEventAsync(eventId))
                .ReturnsAsync(_event);

            this.eventsRegistrationsRepository
                .Setup(i => i.CountEventRegistrationsAsync(_event.Id))
                .ReturnsAsync(maxAttendance);


            //Act && Assert
            await Assert.ThrowsAsync<EventSoldOutException>(async () => await this.target.RegisterAsync(eventId, registrationDto));
        }


        private IMapper SetupMapper()
        {
            var profiles = new Profile[]
        {
                new EventsProfile(),
                new RegistrationProfile()
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
