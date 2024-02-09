using System.Linq.Expressions;
using SampleSmartLockApp.Application.Features.AccessPermissions.Queries.GetAllAccessPermissions;

namespace SampleSmartLockApp.Application.Tests.Features.AccessPermissions.Queries
{
    public class GetAllAccessPermissionsQueryHandlerTests
    {
        private readonly Mock<IAccessPermissionRepositoryAsync> accessPermissionRepositoryAsync = new();

        [Fact]
        public async Task GetAllPaginatedFilteredAsyncMethodWithEmptyResultShouldReturnPagedSucessAsync()
        {
            // Arrange
            var query = new GetAllAccessPermissionsQuery(1, 10);

            accessPermissionRepositoryAsync
                .Setup(aph => aph.GetAllPaginatedFilteredAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<AccessPermission, bool>>?>()))
                .ReturnsAsync([])
                .Verifiable();

            var handler = new GetAllAccessPermissionsQueryHandler(accessPermissionRepositoryAsync.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Message.Should().Be($"No records found.");
            result.Data.Should().BeNull();
            result.Succeeded.Should().Be(false);
            result.PageNumber!.Should().Be(1);
            result.PageSize!.Should().Be(10);
            accessPermissionRepositoryAsync.Object.Should().BeAssignableTo<IAccessPermissionRepositoryAsync>();
        }
    }
}