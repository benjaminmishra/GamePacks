using GamePacks.DataAccess;
using GamePacks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePacks.Service.Tests;

[Trait("Type", "Integration")]
public class PackRepositoryTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;
    private readonly GamePacksDbContext _dbContext;
    private readonly IPackRepository _packRepository;

    public PackRepositoryTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _dbContext = _fixture.GetDbContextInstance();
        _packRepository = new PackRepository(_dbContext);
    }

    [Fact]
    public async Task AddPackAsync_ShouldAddPackToDatabase()
    {
        var newPack = new Pack
        {
            Id = Guid.NewGuid(),
            Name = "Test Pack",
            IsActive = true,
            Price = 200
        };

        var addedPack = await _packRepository.AddPackAsync(newPack, TestHelpers.CreateCancellationToken());

        Assert.NotNull(addedPack);
        Assert.Equal(newPack.Name, addedPack.Name);

        var packFromDb = await _dbContext.Packs.FindAsync(newPack.Id);
        Assert.NotNull(packFromDb);
        Assert.Equal(newPack.Name, packFromDb.Name);
    }

    [Fact]
    public async Task GetPackByIdAsync_ShouldReturnPackWithDetails()
    {
        var packId = (await _dbContext.Packs.FirstAsync()).Id;

        var retrievedPack = await _packRepository.GetPackByIdAsync(packId, TestHelpers.CreateCancellationToken());

        Assert.NotNull(retrievedPack);
        Assert.NotEmpty(retrievedPack.PackItems);
    }

    [Fact]
    public async Task GetAllPacksAsync_ShouldReturnAllPacks()
    {
        var packs = await _packRepository.GetAllPacksAsync(TestHelpers.CreateCancellationToken());

        Assert.NotNull(packs);
        Assert.True(packs.Any());
    }

    [Fact]
    public async Task PackItemExistsByNameAsync_ShouldReturnTrue_WhenItemExists()
    {
        var exists = await _packRepository.PackItemExistsByNameAsync("The Desk", TestHelpers.CreateCancellationToken());

        Assert.True(exists);
    }

    [Fact]
    public async Task PackItemExistsByNameAsync_ShouldReturnFalse_WhenItemDoesNotExist()
    {
        var exists = await _packRepository.PackItemExistsByNameAsync("Non-existing Item", TestHelpers.CreateCancellationToken());

        Assert.False(exists);
    }
}
