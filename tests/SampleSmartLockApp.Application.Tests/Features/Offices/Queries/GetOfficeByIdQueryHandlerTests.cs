using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SampleSmartLockApp.Application.Features.Offices.Queries.GetAll;
using SampleSmartLockApp.Application.Interfaces.Repositories;
using SampleSmartLockApp.Application.Wrappers;
using SampleSmartLockApp.Domain.Entities;
using Xunit;

namespace SampleSmartLockApp.Application.Tests.Features.Offices.Queries
{
    public class GetOfficeByIdQueryHandlerTests
    {
        private readonly Mock<IOfficeRepositoryAsync> mockOfficeRepository = new();

        [Fact]
        public async void GetByIdAsyncMethodWithResultShouldReturnApiResponseSucess()
        {
            // Arrange
            var query = new GetOfficeByIdQuery(Guid.NewGuid());

            mockOfficeRepository
                .Setup(r => r.GetByIdAsync(query.Id))
                .ReturnsAsync(new Office { Id = query.Id, Name = "Test Office" })
                .Verifiable();

            var handler = new GetOfficeByIdQueryHandler(mockOfficeRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockOfficeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
            result.Data!.Name.Should().Be("Test Office");
            result.Data!.Id.Should().Be(query.Id);
            result.Should().BeOfType<ApiResponse<GetOfficeByIdViewModel>>();
            result.Succeeded.Should().Be(true);
            result.Message.Should().BeNull();
        }

        [Fact]
        public async void GetByIdAsyncMethodWithNullResultShouldReturnApiResponseFail()
        {
            // Arrange
            var query = new GetOfficeByIdQuery(Guid.NewGuid());

            mockOfficeRepository
                .Setup(r => r.GetByIdAsync(query.Id))
                .Returns(Task.FromResult<Office?>(null!))
                .Verifiable();

            var handler = new GetOfficeByIdQueryHandler(mockOfficeRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockOfficeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
            result.Message!.Should().Be($"Office with id: {query.Id} not found!");
            result.Data.Should().BeNull();
            result.Succeeded.Should().Be(false);
            result.Should().BeOfType<ApiResponse<GetOfficeByIdViewModel>>();
        }
    }
}