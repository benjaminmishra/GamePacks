using GamePacks.DataAccess;
using GamePacks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class DataSeeder
{
    private readonly GamePacksDbContext _gamePacksDbContext;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(GamePacksDbContext gamePacksDbContext, ILogger<DataSeeder> logger)
    {
        _gamePacksDbContext = gamePacksDbContext;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        // Avoid seeding duplicates
        if (await _gamePacksDbContext.Packs.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Data seeding skipped, packs already exist.");
            return;
        }

        // Classroom Pack
        var classRoomPack = new Pack
        {
            Id = Guid.NewGuid(),
            Name = "The Classroom Pack",
            ShortName = "pack.classroom",
            Price = 10,
            IsActive = true,
            ChildPacks = new List<Pack>()
        };

        var deskPackItem = new PackItem
        {
            Id = Guid.NewGuid(),
            Category = "classroom",
            Name = "The Desk",
            OwnerPack = classRoomPack
        };

        var chairPackItem = new PackItem
        {
            Id = Guid.NewGuid(),
            Category = "classroom",
            Name = "The Chair",
            OwnerPack = classRoomPack
        };

        var boardPackItem = new PackItem
        {
            Id = Guid.NewGuid(),
            Category = "classroom",
            Name = "The Blackboard",
            OwnerPack = classRoomPack
        };

        // School Pack (Parent pack of Classroom Pack)
        var schoolPack = new Pack
        {
            Id = Guid.NewGuid(),
            Name = "The School Pack",
            ShortName = "pack.school",
            Price = 20,
            IsActive = true,
            ChildPacks = new List<Pack> { classRoomPack }
        };

        var playgroundPackItem = new PackItem
        {
            Id = Guid.NewGuid(),
            Category = "school",
            Name = "The Playground",
            OwnerPack = schoolPack
        };

        var libraryPackItem = new PackItem
        {
            Id = Guid.NewGuid(),
            Category = "school",
            Name = "The Library",
            OwnerPack = schoolPack
        };

        _gamePacksDbContext.PackItems.AddRange(deskPackItem, chairPackItem, boardPackItem, playgroundPackItem, libraryPackItem);
        _gamePacksDbContext.Packs.AddRange(classRoomPack, schoolPack);

        await _gamePacksDbContext.SaveChangesAsync(cancellationToken);
    }
}
