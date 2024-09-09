using GamePacks.DataAccess.Models;
using GamePacks.DataAccess;
using OneOf;
using OneOf.Types;

namespace GamePacks.Service.UseCases;

public class GetPackByIdQueryHandler
{
    private readonly IPackRepository _packRepository;

    public GetPackByIdQueryHandler(IPackRepository packRepository)
    {
        _packRepository = packRepository;
    }

    public async Task<OneOf<Pack,Error>> ExecuteAsync(Guid packId) 
    {
        try
        { 
            var pack = await _packRepository.GetPackByIdAsync(packId);
            if(pack is null)
                return new Error();

            return pack;
        }
        catch (Exception)
        {
            return new Error();
        }
    }
}