using GamePacks.DataAccess.Models;

namespace GamePacks.DataAccess;

public interface IPackRepository
{
    Task<Pack> AddPackAsync(Pack newPack, CancellationToken cancellationToken);

    Task<Pack?> GetPackByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Pack>> GetAllPacksAsync(CancellationToken cancellationToken);

    Task<bool> PackItemExistsByNameAsync(string name, CancellationToken cancellationToken);
}