using SampleSmartLockApp.Application.Features.Offices.Commands.Delete;

namespace SampleSmartLockApp.Application.Tests.Features.Offices.Commands
{
    public class DeleteOfficeCommandHandlerTests
    {
        private readonly Mock<IOfficeRepositoryAsync> mockOfficeRepository = new();

        [Fact]
        public async void DeleteAsyncMethodWithResultShouldReturnApiResponseSuccess()
        {
            // Arrange
            var command = new DeleteOfficeCommand(Guid.NewGuid());
            var office = new Office { Id = command.Id, Name = "Test Office" };

            mockOfficeRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(office)
                .Verifiable();

            mockOfficeRepository
                .Setup(r => r.DeleteAsync(It.IsAny<Office>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new DeleteOfficeCommandHandler(mockOfficeRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Data.Should().Be(office.Id);
            result.Succeeded.Should().Be(true);
            mockOfficeRepository.Verify(m => m.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            mockOfficeRepository.Verify(m => m.DeleteAsync(office), Times.Once());
            mockOfficeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
        }
    }
}