using SampleSmartLockApp.Application.Features.Locks.Commands.Create;

namespace SampleSmartLockApp.Application.Tests.Features.Locks.Commands
{
    public class CreateLockCommandHandlerTests
    {
        private readonly Mock<ILockRepositoryAsync> lockRepository = new();
        private readonly Mock<IOfficeRepositoryAsync> officeRepository = new();


        [Fact]
        public async void GetByIdAsyncWithNoOfficeResultShouldReturnApiResponseFail()
        {
            // Arrange
            var command = new CreateLockCommand("Test Lock", Guid.NewGuid());

            lockRepository
                .Setup(ap => ap.AddAsync(It.IsAny<Lock>()))
                .ReturnsAsync(It.IsAny<Lock>())
                .Verifiable();

            officeRepository
                .Setup(ap => ap.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Office?>(null))
                .Verifiable();

            var handler = new CreateLockCommandHandler(lockRepository.Object, officeRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Message.Should().Be("Office not found.");
            result.Succeeded.Should().Be(false);
            lockRepository.Object.Should().BeAssignableTo<ILockRepositoryAsync>();
            officeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
            lockRepository.Verify(ap => ap.AddAsync(It.IsAny<Lock>()), Times.Never);
            officeRepository.Verify(ap => ap.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}