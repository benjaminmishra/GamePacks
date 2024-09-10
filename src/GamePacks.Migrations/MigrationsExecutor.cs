using GamePacks.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GamePacks.Migrations;

public sealed class MigrationsExecutor
{
    private readonly GamePacksDbContext _gamePacksDbContext;

    public MigrationsExecutor(GamePacksDbContext gamePacksDbContext)
    {
        _gamePacksDbContext = gamePacksDbContext;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken) => await _gamePacksDbContext.Database.MigrateAsync(cancellationToken);
}