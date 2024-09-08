using System.Runtime.CompilerServices;
using GamePacks.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GamePacks.Migrations;

public sealed class MigrationsExecutor : IDisposable
{
    private readonly GamePacksDbContext _gamePacksDbContext;

    public MigrationsExecutor(GamePacksDbContext gamePacksDbContext)
    {
        _gamePacksDbContext = gamePacksDbContext;
    }

    public void Execute()
    {
        _gamePacksDbContext.Database.Migrate();
    }

    public void Dispose()
    {
        _gamePacksDbContext.Dispose();
    }
}