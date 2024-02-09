using SampleSmartLockApp.Application.Features.Offices.Commands.Update;

namespace SampleSmartLockApp.Application.Tests.Features.Offices.Commands
{
    public class UpdateOfficeCommandHandlerTests
    {
        private readonly Mock<IOfficeRepositoryAsync> mockOfficeRepository = new();

        [Fact]
        public async void UpdateAsyncMethodWithResultShouldReturnApiResponseSuccess()
        {
            // Arrange
            var command = new UpdateOfficeCommand(Guid.NewGuid(), "Test Office 2");
            var office = new Office { Name = "Test Office" };

            mockOfficeRepository
               .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
               .ReturnsAsync(office)
               .Verifiable();

            mockOfficeRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Office>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UpdateOfficeCommandHandler(mockOfficeRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Data.Should().Be(command.Id);
            result.Succeeded.Should().Be(true);
            mockOfficeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
            mockOfficeRepository.Verify(m => m.GetByIdAsync(It.Is<Guid>(v => v.Equals(command.Id))), Times.Once());
            mockOfficeRepository.Verify(m => m.UpdateAsync(It.Is<Office>(e => e.Name == command.Name)), Times.Once());
        }
    }
}