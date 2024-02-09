using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleSmartLockApp.Application.Features.AccessPermissions.Commands.Create;
using SampleSmartLockApp.Infrastructure.Contexts;

namespace SampleSmartLockApp.Application.Tests.Features.AccessPermissions.Commands
{
    public class GrantLockAccessToUserCommandHandlerTests
    {
        private readonly Mock<IAccessPermissionRepositoryAsync> accessPermissionRepository = new();
        private readonly Mock<ILockRepositoryAsync> lockRepository = new();
        private readonly Mock<IUserStore<ApplicationUser>> mockUserStore = new();
        private readonly Mock<UserManager<ApplicationUser>> userManager = new();

        public GrantLockAccessToUserCommandHandlerTests()
        {
            userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null!, null!, null!, null!, null!, null!, null!, null!)
            {
                CallBase = true
            };
        }

        [Fact]
        public async void GetLockAccessOfUserWithNoUserResultShouldReturnApiResponseFail()
        {
            var command = new GrantLockAccessToUserCommand(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.UtcNow);
            var accessPermission = new AccessPermission
            {
                UserId = command.UserId,
                LockId = command.LockId,
                ValidUntil = command.ValidUntil
            };

            userManager
                .Setup(ap => ap.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser?>(null!))
                .Verifiable();

            lockRepository
                .Setup(ap => ap.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Lock?>(null!))
                .Verifiable();

            accessPermissionRepository
                .Setup(ap => ap.GetLockAccessOfUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult<AccessPermission?>(null!))
                .Verifiable();

            accessPermissionRepository
                .Setup(ap => ap.AddAsync(It.IsAny<AccessPermission>()))
                .ReturnsAsync(accessPermission)
                .Verifiable();

            var handler = new GrantLockAccessToUserCommandHandler(accessPermissionRepository.Object, lockRepository.Object, userManager.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Message.Should().Be("User with provided UserId not found.");
            result.Succeeded.Should().Be(false);
            accessPermissionRepository.Object.Should().BeAssignableTo<IAccessPermissionRepositoryAsync>();
            lockRepository.Object.Should().BeAssignableTo<ILockRepositoryAsync>();
            userManager.Object.Should().BeAssignableTo<UserManager<ApplicationUser>>();

            accessPermissionRepository.Verify(ap => ap.GetLockAccessOfUser(It.Is<Guid>(v => v.Equals(command.UserId)), It.Is<Guid>(v => v.Equals(command.LockId))), Times.Never);
            userManager.Verify(ap => ap.FindByIdAsync(It.IsAny<string>()), Times.Once);
            lockRepository.Verify(ap => ap.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            accessPermissionRepository.Verify(ap => ap.AddAsync(It.IsAny<AccessPermission>()), Times.Never);
        }
    }
}