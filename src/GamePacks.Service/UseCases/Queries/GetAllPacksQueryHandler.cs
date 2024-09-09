using GamePacks.DataAccess.Models;
using GamePacks.Service.Endpoints;
using GamePacks.DataAccess;
using OneOf;
using OneOf.Types;
using GamePacks.Service.Models;

namespace GamePacks.Service.UseCases;

public class GetAllPacksQueryHandler
{
    private readonly IPackRepository _packRepository;

    public GetAllPacksQueryHandler(IPackRepository packRepository)
    {
        _packRepository = packRepository;
    }

    public async Task<OneOf<IEnumerable<Pack>,PackError>> ExecuteAsync() 
    {
        try
        { 
            var allPacks = await _packRepository.GetAllPacksAsync();
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