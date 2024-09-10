using GamePacks.DataAccess;
using GamePacks.DataAccess.Models;
using GamePacks.Service.Models.Errors;
using GamePacks.Service.UseCases.Queries;
using Moq;

namespace GamePacks.Service.Tests;

public class GetAllPacksQueryHandlerTests
{
    private readonly Mock<IPackRepository> _packRepositoryMock;
    private readonly GetAllPacksQueryHandler _handler;

    public GetAllPacksQueryHandlerTests()
    {
        _packRepositoryMock = new Mock<IPackRepository>();
        _handler = new GetAllPacksQueryHandler(_packRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnAllPacks_WhenPacksExist()
    {
        _packRepositoryMock
            .Setup(repo => repo.GetAllPacksAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(
            [
                new Pack { Id = Guid.NewGuid(), Name = "Pack 1", IsActive = true, Price = 10 },
                new Pack { Id = Guid.NewGuid(), Name = "Pack 2", IsActive = true, Price = 20 }
            ]);

        var result = await _handler.ExecuteAsync(TestHelpers.CreateCancellationToken());

        Assert.True(result.IsT0);
        Assert.NotNull(result.AsT0);
        Assert.Equal(2, result.AsT0.Count());
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPackNotFoundError_WhenNoPacksExist()
    {
        _packRepositoryMock
            .Setup(repo => repo.GetAllPacksAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(Array.Empty<Pack>());

        var result = await _handler.ExecuteAsync(TestHelpers.CreateCancellationToken());

        Assert.True(result.IsT1);
        Assert.NotNull(result.AsT1);
        Assert.IsType<PackNotFoundError>(result.AsT1);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPackExceptionError_WhenExceptionIsThrown()
    {
        _packRepositoryMock
            .Setup(repo => repo.GetAllPacksAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        var result = await _handler.ExecuteAsync(TestHelpers.CreateCancellationToken());

        Assert.True(result.IsT1);
        Assert.NotNull(result.AsT1);
        Assert.IsType<PackExceptionError>(result.AsT1);
    }
}
