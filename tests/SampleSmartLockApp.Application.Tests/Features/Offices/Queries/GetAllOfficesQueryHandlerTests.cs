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
    public class GetAllOfficesQueryHandlerTests
    {
        private readonly Mock<IOfficeRepositoryAsync> mockOfficeRepository = new();

        [Fact]
        public async void GetAllPaginatedAsyncMethodWithEmptyResultShouldReturnPagedSucess()
        {
            // Arrange
            var query = new GetAllOfficesQuery(1, 10);

            mockOfficeRepository
                .Setup(r => r.GetAllPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync([])
                .Verifiable();

            var handler = new GetAllOfficesQueryHandler(mockOfficeRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data!.Count().Should().Be(0);
            result.PageNumber!.Should().Be(1);
            result.PageSize!.Should().Be(10);
            result.Succeeded.Should().Be(true);
            mockOfficeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
        }

        [Fact]
        public async void GetAllPaginatedAsyncMethodWithNullResultShouldReturnPagedFail()
        {
            // Arrange
            var query = new GetAllOfficesQuery(1, 10);

            mockOfficeRepository
                .Setup(r => r.GetAllPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult<IReadOnlyList<Office>>(null!))
                .Verifiable();

            var handler = new GetAllOfficesQueryHandler(mockOfficeRepository.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Message!.Should().Be("No records found!");
            result.PageNumber!.Should().Be(1);
            result.PageSize!.Should().Be(10);
            result.Succeeded.Should().Be(false);
            mockOfficeRepository.Object.Should().BeAssignableTo<IOfficeRepositoryAsync>();
        }
    }
}