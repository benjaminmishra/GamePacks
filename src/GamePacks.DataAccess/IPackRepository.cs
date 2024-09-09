using GamePacks.DataAccess.Models;

namespace GamePacks.DataAccess;

public interface IPackRepository
{
    Task<Pack> AddPackAsync(Pack newPack);

    Task<Pack?> GetPackByIdAsync(Guid id);

    Task<IEnumerable<Pack>> GetAllPacksAsync();
}