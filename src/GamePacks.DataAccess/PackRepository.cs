using GamePacks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePacks.DataAccess;

public class PackRepository : IPackRepository
{
    private readonly GamePacksDbContext _gamePacksDbContext;

    public PackRepository(GamePacksDbContext gamePacksDbContext)
    {
        _gamePacksDbContext = gamePacksDbContext;
    }

    // Add a new Pack to the database and return the result
    public async Task<Pack> AddPackAsync(Pack newPack, CancellationToken cancellationToken)
    {
        await _gamePacksDbContext.Packs.AddAsync(newPack,cancellationToken);
        await _gamePacksDbContext.SaveChangesAsync(cancellationToken);
        return newPack;
    }

    // Get a Pack by its ID and all its related details
    public async Task<Pack?> GetPackByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _gamePacksDbContext.Packs
            .Include(p => p.PackItems)
            .Include(p => p.ChildPacks)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    // Get all Packs from the database
    public async Task<IEnumerable<Pack>> GetAllPacksAsync(CancellationToken cancellationToken)
    {
        return await _gamePacksDbContext.Packs.ToListAsync(cancellationToken);
    }

    public async Task<bool> PackItemExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _gamePacksDbContext
        .PackItems
        .AnyAsync(p => p.Name.ToLower() == name.ToLower(), cancellationToken);
    }
}
