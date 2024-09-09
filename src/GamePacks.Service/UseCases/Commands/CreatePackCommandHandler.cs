using GamePacks.DataAccess.Models;
using GamePacks.Service.Endpoints;
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

    public async Task<OneOf<Pack,Error>> ExecuteAsync(CreatePackRequest command)
    {
        var newPack = new Pack {
            Name = command.PackName,
            IsActive = command.Active,
            ShortName = $"pack.{command.PackName.Replace(" ", string.Empty).ToLower()}",
            Price = command.Price,
        };

        return await _packRepository.AddPackAsync(newPack);
    }
}