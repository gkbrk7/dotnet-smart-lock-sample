using SampleSmartLockApp.Application.Features.Locks.Queries.GetAll;

namespace SampleSmartLockApp.Application.Tests.Features.Locks.Queries
{
    public class GetLockByIdQueryHandlerTests
    {

        private readonly Mock<ILockRepositoryAsync> lockRepository = new();

        [Fact]
        public async void GetByLockIdWithOfficeAsyncMethodWithNullResultShouldReturnApiResponseFail()
        {
            // Arrange
            var query = new GetLockByIdQuery(Guid.NewGuid());

            lockRepository
                .Setup(r => r.GetByLockIdWithOfficeAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Lock?>(null))
                .Verifiable();

            var handler = new GetLockByIdQueryHandler(lockRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Message!.Should().Be($"Lock with id: {query.Id} not found!");
            result.Succeeded.Should().Be(false);
            result.Data.Should().BeNull();
            lockRepository.Object.Should().BeAssignableTo<ILockRepositoryAsync>();
        }
    }
}