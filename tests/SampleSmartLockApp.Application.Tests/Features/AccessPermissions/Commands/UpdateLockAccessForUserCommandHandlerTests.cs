using SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Update;

namespace SampleSmartLockApp.Application.Tests.Features.AccessPermissions.Commands
{
    public class UpdateLockAccessForUserCommandHandlerTests
    {
        private readonly Mock<IAccessPermissionRepositoryAsync> accessPermissionRepositoryAsync = new();

        [Fact]
        public async void GetLockAccessOfUserWithNoUserResultShouldReturnApiResponseFail()
        {
            // Arrange
            var command = new UpdateLockAccessForUserCommand(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.UtcNow);

            accessPermissionRepositoryAsync
                .Setup(ap => ap.GetLockAccessOfUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult<AccessPermission?>(null!))
                .Verifiable();

            accessPermissionRepositoryAsync
                .Setup(ap => ap.UpdateAsync(It.IsAny<AccessPermission>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UpdateLockAccessForUserCommandHandler(accessPermissionRepositoryAsync.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Message.Should().Be("Access permission not found!");
            result.Succeeded.Should().Be(false);
            accessPermissionRepositoryAsync.Object.Should().BeAssignableTo<IAccessPermissionRepositoryAsync>();
            accessPermissionRepositoryAsync.Verify(ap => ap.GetLockAccessOfUser(command.UserId, command.LockId), Times.Once);
            accessPermissionRepositoryAsync.Verify(ap => ap.UpdateAsync(It.IsAny<AccessPermission>()), Times.Never);
        }
    }
}