using SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Delete;

namespace SampleSmartLockApp.Application.Tests.Features.AccessPermissions.Commands
{
    public class RevokeLockAccessFromUserCommandCommandHandlerTests
    {
        private readonly Mock<IAccessPermissionRepositoryAsync> accessPermissionRepositoryAsync = new();

        [Fact]
        public async void GetLockAccessOfUserWithNoUserResultShouldReturnApiResponseFail()
        {
            // Arrange
            var command = new RevokeLockAccessFromUserCommand(Guid.NewGuid(), Guid.NewGuid());

            accessPermissionRepositoryAsync
                .Setup(ap => ap.GetLockAccessOfUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult<AccessPermission?>(null!))
                .Verifiable();

            accessPermissionRepositoryAsync
                .Setup(ap => ap.DeleteAsync(It.IsAny<AccessPermission>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new RevokeLockAccessFromUserCommandHandler(accessPermissionRepositoryAsync.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Message.Should().Be("Access permission not found!");
            result.Succeeded.Should().Be(false);
            accessPermissionRepositoryAsync.Object.Should().BeAssignableTo<IAccessPermissionRepositoryAsync>();
            accessPermissionRepositoryAsync.Verify(ap => ap.GetLockAccessOfUser(command.UserId, command.LockId), Times.Once);
            accessPermissionRepositoryAsync.Verify(ap => ap.DeleteAsync(It.IsAny<AccessPermission>()), Times.Never);
        }
    }
}