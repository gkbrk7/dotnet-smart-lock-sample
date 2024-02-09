using SampleSmartLockApp.Application.Features.Locks.Queries.GetAll;

namespace SampleSmartLockApp.Application.Tests.Features.Locks.Queries
{
    public class GetAllLocksQueryHandlerTests
    {
        private readonly Mock<ILockRepositoryAsync> lockRepository = new();

        [Fact]
        public async void GetAllWithOfficePaginatedAsyncMethodWithEmptyResultShouldReturnPagedFail()
        {
            // Arrange
            var query = new GetAllLocksQuery(1, 10);

            lockRepository
                .Setup(r => r.GetAllWithOfficePaginatedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync([])
                .Verifiable();

            var handler = new GetAllLocksQueryHandler(lockRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Message!.Should().Be("No records found!");
            result.PageNumber!.Should().Be(1);
            result.PageSize!.Should().Be(10);
            result.Succeeded.Should().Be(false);
            lockRepository.Object.Should().BeAssignableTo<ILockRepositoryAsync>();
        }
    }
}