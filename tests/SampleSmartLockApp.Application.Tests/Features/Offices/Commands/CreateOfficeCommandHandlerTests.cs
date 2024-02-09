using SampleSmartLockApp.Application.Features.Offices.Commands.Create;

namespace SampleSmartLockApp.Application.Tests.Features.Offices.Commands
{
    public class CreateOfficeCommandHandlerTests
    {
        private readonly Mock<IOfficeRepositoryAsync> mockOfficeRepository = new();

        [Fact]
        public async void AddAsyncMethodWithResultShouldReturnApiResponseSuccess()
        {
            // Arrange
            var command = new CreateOfficeCommand("Test Office");
            var office = new Office { Id = Guid.NewGuid(), Name = command.Name };

            mockOfficeRepository
                .Setup(r => r.AddAsync(It.IsAny<Office>()))
                .ReturnsAsync(office)
                .Verifiable();

            var handler = new CreateOfficeCommandHandler(mockOfficeRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Data.Should().Be(office.Id);
            result.Succeeded.Should().Be(true);
            mockOfficeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
        }
    }
}