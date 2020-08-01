namespace Application.Services.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using ApplicationDTO;
    using ApplicationServices.Registrations;
    using AutoFixture;
    using Xunit;

    [ExcludeFromCodeCoverage]
    public class RegistrationsServiceTests
    {
        private RegistrationsService target;
        private Fixture fixture;

        public RegistrationsServiceTests()
        {
            this.fixture = new Fixture();
            this.target = new RegistrationsService();
        }

        [Fact]
        public async Task RegisteAsync_Id_Registration_RegistrationId()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registration = this.fixture.Create<RegistrationDto>();

            //Act
            var result = await this.target.RegisteAsync(eventId, registration);

            //Assert
        }

        [Fact]
        public async Task RegisteAsync_IdDoesntExist_Registration_ThrowsNotFoundException()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registration = this.fixture.Create<RegistrationDto>();

            //Act
            var result = await this.target.RegisteAsync(eventId, registration);

            //Assert
        }

        [Fact]
        public async Task RegisteAsync_EventSoldOut_ThrowsEventSoldOutException()
        {
            //Arrange
            var eventId = this.fixture.Create<Guid>();
            var registration = this.fixture.Create<RegistrationDto>();

            //Act
            var result = await this.target.RegisteAsync(eventId, registration);

            //Assert
        }
    }
}
