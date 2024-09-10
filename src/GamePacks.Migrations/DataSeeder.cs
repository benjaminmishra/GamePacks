using GamePacks.DataAccess;
using GamePacks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

public class DataSeeder
{
    private readonly GamePacksDbContext _gamePacksDbContext;

    public DataSeeder(GamePacksDbContext gamePacksDbContext)
    {
        _gamePacksDbContext = gamePacksDbContext;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        if (await _gamePacksDbContext.Packs.AnyAsync(cancellationToken))
            return;

        // Classroom Pack
        var classRoomPack = new Pack
        {
            Name = "The Classroom Pack",
            Price = 10,
            IsActive = true,
            ChildPacks = []
        };

        var deskPackItem = new PackItem
        {
            Name = "The Desk",
            OwnerPack = classRoomPack
        };

        var chairPackItem = new PackItem
        {
            Name = "The Chair",
            OwnerPack = classRoomPack
        };

        var boardPackItem = new PackItem
        {
            Name = "The Blackboard",
            OwnerPack = classRoomPack
        };

        // School Pack (Parent pack of Classroom Pack)
        var schoolPack = new Pack
        {
            Name = "The School Pack",
            Price = 20,
            IsActive = true,
            ChildPacks = [classRoomPack]
        };

        var playgroundPackItem = new PackItem
        {
            Name = "The Playground",
            OwnerPack = schoolPack
        };

        var libraryPackItem = new PackItem
        {
            Name = "The Library",
            OwnerPack = schoolPack
        };

        _gamePacksDbContext.PackItems.AddRange(deskPackItem, chairPackItem, boardPackItem, playgroundPackItem, libraryPackItem);
        _gamePacksDbContext.Packs.AddRange(classRoomPack, schoolPack);

        await _gamePacksDbContext.SaveChangesAsync(cancellationToken);
    }
}
