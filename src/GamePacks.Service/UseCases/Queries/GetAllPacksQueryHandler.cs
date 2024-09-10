using GamePacks.DataAccess.Models;
using GamePacks.DataAccess;
using OneOf;
using GamePacks.Service.Models;

namespace GamePacks.Service.UseCases;

public class GetAllPacksQueryHandler
{
    private readonly IPackRepository _packRepository;

    public GetAllPacksQueryHandler(IPackRepository packRepository)
    {
        _packRepository = packRepository;
    }

    public async Task<OneOf<IEnumerable<Pack>,PackError>> ExecuteAsync(CancellationToken cancellationToken) 
    {
        try
        { 
            var allPacks = await _packRepository.GetAllPacksAsync(cancellationToken);
            if(!allPacks.Any())
                return new PackNotFoundError("No packs found in the database");

            return allPacks.ToList();
        }
        catch (Exception ex)
        {
            return new PackExceptionError(ex);
        }
    }
}