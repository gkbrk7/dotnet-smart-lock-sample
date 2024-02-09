using SampleSmartLockApp.Application.Features.AccessPermissionHistories.Commands;

namespace SampleSmartLockApp.Application.Tests.Features.AccessPermissionHistories.Commands
{
    public class CreateAccessPermissionsHistoryCommandHandlerTests
    {
        private readonly Mock<IAccessPermissionHistoryRepositoryAsync> accessPermissionHistoryRepository = new();

        [Fact]
        public async void AddAsyncMethodWithResultShouldReturnApiResponseSuccess()
        {
            // Arrange
            var entity = new AccessPermissionHistory
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                LockId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.UtcNow,
                Message = "Test Message",
                IsConfirmed = true
            };

            var command = new CreateAccessPermissionsHistoryCommand(entity.UserId, entity.LockId, entity.Timestamp, entity.IsConfirmed, entity.Message);


            accessPermissionHistoryRepository
                .Setup(aph => aph.AddAsync(It.IsAny<AccessPermissionHistory>()))
                .ReturnsAsync(entity)
                .Verifiable();

            var handler = new CreateAccessPermissionsHistoryCommandHandler(accessPermissionHistoryRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeOfType<ApiResponse<CreateAccessPermissionsHistoryViewModel>>();
            result.Data!.Id.Should().Be(entity.Id);
            result.Data!.UserId.Should().Be(entity.UserId);
            result.Data!.LockId.Should().Be(entity.LockId);
            result.Data!.Timestamp.Should().Be(entity.Timestamp);
            result.Data!.Message.Should().Be(entity.Message);
            result.Data!.IsConfirmed.Should().Be(entity.IsConfirmed);
            result.Succeeded.Should().Be(true);
            result.Message.Should().Be(null);
        }
    }
}