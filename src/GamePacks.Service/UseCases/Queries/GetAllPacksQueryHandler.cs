using GamePacks.DataAccess;
using GamePacks.DataAccess.Models;
using GamePacks.Service.Models.Errors;
using OneOf;

namespace GamePacks.Service.UseCases.Queries;

public class GetAllPacksQueryHandler
{
    private readonly IPackRepository _packRepository;

    public GetAllPacksQueryHandler(IPackRepository packRepository)
    {
        _packRepository = packRepository;
    }

    public async Task<OneOf<IEnumerable<Pack>, PackError>> ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            var allPacks = await _packRepository.GetAllPacksAsync(cancellationToken);
            if (!allPacks.Any())
                return new PackNotFoundError("No packs found in the database");

            return allPacks.ToList();
        }
        catch (Exception ex)
        {
            return new PackExceptionError(ex);
        }
    }
}