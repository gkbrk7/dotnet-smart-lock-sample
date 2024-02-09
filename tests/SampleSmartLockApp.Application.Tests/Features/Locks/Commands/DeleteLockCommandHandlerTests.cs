using SampleSmartLockApp.Application.Features.Locks.Commands.Delete;

namespace SampleSmartLockApp.Application.Tests.Features.Locks.Commands
{
    public class DeleteLockCommandHandlerTests
    {
        private readonly Mock<ILockRepositoryAsync> lockRepository = new();

        [Fact]
        public async void GetByIdAsyncWithNoOfficeResultShouldReturnApiResponseFail()
        {
            // Arrange
            var command = new DeleteLockCommand(Guid.NewGuid());

            lockRepository
                .Setup(ap => ap.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Lock?>(null!))
                .Verifiable();


            var handler = new DeleteLockCommandHandler(lockRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Message.Should().Be("Lock not found.");
            result.Succeeded.Should().Be(false);
            lockRepository.Object.Should().BeAssignableTo<ILockRepositoryAsync>();
            lockRepository.Verify(ap => ap.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}