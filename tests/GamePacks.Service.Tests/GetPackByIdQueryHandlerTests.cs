using GamePacks.DataAccess;
using GamePacks.DataAccess.Models;
using GamePacks.Service.Models;
using GamePacks.Service.UseCases;
using Moq;

namespace GamePacks.Service.Tests;

[Trait("Type","Integration")]
public class GetPackByIdQueryHandlerTests
{
    private readonly Mock<IPackRepository> _packRepositoryMock;
    private readonly GetPackByIdQueryHandler _handler;

    public GetPackByIdQueryHandlerTests()
    {
        _packRepositoryMock = new Mock<IPackRepository>();
        _handler = new GetPackByIdQueryHandler(_packRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPack_WhenPackExists()
    {
        var packId = Guid.NewGuid();
        var pack = new Pack
        {
            Id = packId,
            Name = "Sample Pack",
            IsActive = true,
            Price = 15
        };

        _packRepositoryMock
            .Setup(repo => repo.GetPackByIdAsync(packId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pack);

        var result = await _handler.ExecuteAsync(packId, TestHelpers.CreateCancellationToken());

        Assert.True(result.IsT0);
        Assert.NotNull(result.AsT0);
        Assert.Equal(packId, result.AsT0.Id);
        Assert.Equal("Sample Pack", result.AsT0.Name);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPackNotFoundError_WhenPackDoesNotExist()
    {
        var packId = Guid.NewGuid();

        _packRepositoryMock
            .Setup(repo => repo.GetPackByIdAsync(packId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Pack?)null);

        var result = await _handler.ExecuteAsync(packId, TestHelpers.CreateCancellationToken());

        Assert.True(result.IsT1);
        Assert.NotNull(result.AsT1);
        Assert.IsType<PackNotFoundError>(result.AsT1);
        Assert.Equal($"Pack with id {packId} not found", result.AsT1.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPackExceptionError_WhenExceptionIsThrown()
    {
        var packId = Guid.NewGuid();

        _packRepositoryMock
            .Setup(repo => repo.GetPackByIdAsync(packId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        var result = await _handler.ExecuteAsync(packId, TestHelpers.CreateCancellationToken());

        Assert.True(result.IsT1);
        Assert.NotNull(result.AsT1);
        Assert.IsType<PackExceptionError>(result.AsT1);
        Assert.Equal("Database error", result.AsT1.Message);
    }
}
