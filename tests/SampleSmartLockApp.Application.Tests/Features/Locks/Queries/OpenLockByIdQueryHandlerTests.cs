using MediatR;
using Microsoft.Extensions.Options;
using SampleSmartLockApp.Application.Enums;
using SampleSmartLockApp.Application.Features.Locks.Queries.OpenLock;
using SampleSmartLockApp.Application.Interfaces;
using SampleSmartLockApp.Domain.Settings;

namespace SampleSmartLockApp.Application.Tests.Features.Locks.Queries
{
    public class OpenLockByIdQueryHandlerTests
    {
        private readonly Mock<ILockRepositoryAsync> lockRepository = new();
        private readonly Mock<IAuthenticatedUserService> authenticatedUserService = new();
        private readonly Mock<IAccessPermissionRepositoryAsync> accessPermissionRepository = new();
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<IOptions<LockAccessOptions>> options = new();

        [Fact]
        public async void GetByLockIdWithOfficeAsyncMethodWithNullResultShouldReturnApiResponseFail()
        {
            // Arrange
            var query = new OpenLockByIdQuery(Guid.NewGuid());

            options.Setup(o => o.Value).Returns(new LockAccessOptions());

            authenticatedUserService.Setup(a => a.UserId).Returns(Guid.NewGuid().ToString());

            lockRepository
                .Setup(ap => ap.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Lock?>(null!))
                .Verifiable();

            accessPermissionRepository
                .Setup(ap => ap.GetLockAccessOfUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult<AccessPermission?>(null!))
                .Verifiable();

            accessPermissionRepository
                .Setup(ap => ap.DeleteAsync(It.IsAny<AccessPermission>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new OpenLockByIdQueryHandler(authenticatedUserService.Object, accessPermissionRepository.Object, mediator.Object, lockRepository.Object, options.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Message!.Should().Be("Lock not found.");
            result.Succeeded.Should().Be(false);
            result.Data.Should().BeNull();
            lockRepository.Object.Should().BeAssignableTo<ILockRepositoryAsync>();
            authenticatedUserService.Object.Should().BeAssignableTo<IAuthenticatedUserService>();
            accessPermissionRepository.Object.Should().BeAssignableTo<IAccessPermissionRepositoryAsync>();
            mediator.Object.Should().BeAssignableTo<IMediator>();
        }
    }
}