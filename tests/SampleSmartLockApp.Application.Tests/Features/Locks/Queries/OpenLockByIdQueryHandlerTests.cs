using MediatR;
using SampleSmartLockApp.Application.Features.Locks.Queries.OpenLock;
using SampleSmartLockApp.Application.Interfaces;

namespace SampleSmartLockApp.Application.Tests.Features.Locks.Queries
{
    public class OpenLockByIdQueryHandlerTests
    {
        private readonly Mock<ILockRepositoryAsync> lockRepository = new();
        private readonly Mock<IAuthenticatedUserService> authenticatedUserService = new();
        private readonly Mock<IAccessPermissionRepositoryAsync> accessPermissionRepository = new();
        private readonly Mock<IMediator> mediator = new();

        [Fact]
        public async void GetByLockIdWithOfficeAsyncMethodWithNullResultShouldReturnApiResponseFail()
        {
            // Arrange
            var query = new OpenLockByIdQuery(Guid.NewGuid());

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

            var handler = new OpenLockByIdQueryHandler(authenticatedUserService.Object, accessPermissionRepository.Object, mediator.Object, lockRepository.Object);

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