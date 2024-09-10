using GamePacks.DataAccess.Models;
using GamePacks.DataAccess;
using OneOf;
using GamePacks.Service.Models;

namespace GamePacks.Service.UseCases;

public class GetPackByIdQueryHandler
{
    private readonly IPackRepository _packRepository;

    public GetPackByIdQueryHandler(IPackRepository packRepository)
    {
        _packRepository = packRepository;
    }

    public async Task<OneOf<Pack,PackError>> ExecuteAsync(Guid packId, CancellationToken cancellationToken) 
    {
        try
        { 
            var pack = await _packRepository.GetPackByIdAsync(packId, cancellationToken);
            if(pack is null)
                return new PackNotFoundError($"Pack with id {packId} not found");

            return pack;
        }
        catch (Exception ex)
        {
            return new PackExceptionError(ex);
        }
    }
}