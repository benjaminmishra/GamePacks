using GamePacks.DataAccess.Models;
using GamePacks.Service.Endpoints;
using GamePacks.DataAccess;
using OneOf;
using OneOf.Types;

namespace GamePacks.Service.UseCases;

public class GetAllPacksQueryHandler
{
    private readonly IPackRepository _packRepository;

    public GetAllPacksQueryHandler(IPackRepository packRepository)
    {
        _packRepository = packRepository;
    }

    public async Task<OneOf<IEnumerable<Pack>,Error>> ExecuteAsync() 
    {
        try
        { 
            var allPacks = await _packRepository.GetAllPacksAsync();
            return allPacks.ToList();
        }
        catch (Exception)
        {
            return new Error();
        }
    }
}