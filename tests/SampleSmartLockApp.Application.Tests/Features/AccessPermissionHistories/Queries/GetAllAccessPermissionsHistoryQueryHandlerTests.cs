using SampleSmartLockApp.Application.Features.AccessPermissionHistories.Queries;

namespace SampleSmartLockApp.Application.Tests.Features.AccessPermissionHistories.Queries
{
    public class GetAllAccessPermissionsHistoryQueryHandlerTests
    {
        private readonly Mock<IAccessPermissionHistoryRepositoryAsync> mockAccessPermissionHistoryRepository = new();

        [Fact]
        public async void GetAllPaginatedAsyncMethodWithEmptyResultShouldReturnPagedSucess()
        {
            // Arrange
            var command = new GetAllAccessPermissionsHistoryQuery(1, 10);

            mockAccessPermissionHistoryRepository
                .Setup(aph => aph.GetAllPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync([])
                .Verifiable();

            var handler = new GetAllAccessPermissionsHistoryQueryHandler(mockAccessPermissionHistoryRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Data!.Count().Should().Be(0);
            result.PageNumber!.Should().Be(1);
            result.PageSize!.Should().Be(10);
            result.Succeeded.Should().Be(true);
            mockAccessPermissionHistoryRepository.Object.Should().BeAssignableTo<IAccessPermissionHistoryRepositoryAsync>();
        }
    }
}