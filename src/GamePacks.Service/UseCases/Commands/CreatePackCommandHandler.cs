using GamePacks.DataAccess.Models;
using GamePacks.Service.Models;
using GamePacks.DataAccess;
using OneOf;
using OneOf.Types;

namespace GamePacks.Service.UseCases;

public class CreatePackCommandHandler
{
    private readonly IPackRepository _packRepository;
    public CreatePackCommandHandler(IPackRepository packRepository)
    {
        _packRepository = packRepository;
    }

    public async Task<OneOf<Pack,PackError>> ExecuteAsync(CreatePackRequest command)
    {
        var newPack = new Pack {
            Name = command.PackName,
            IsActive = command.Active,
            ShortName = $"pack.{command.PackName.Replace(" ", string.Empty).ToLower()}",
            Price = command.Price,
        };

        try 
        {
            var addedPack = await _packRepository.AddPackAsync(newPack);
            return addedPack;
        }
        catch (Exception ex) 
        {
            return new PackExceptionError(ex);
        }
    }
}