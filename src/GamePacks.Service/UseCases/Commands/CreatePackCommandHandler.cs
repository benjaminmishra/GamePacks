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

    public async Task<OneOf<Pack, PackError>> ExecuteAsync(CreatePackRequest command, CancellationToken cancellationToken)
    {
        if(command.Price < 0 )
            return new PackValidationError("Pack price cannot be negetive");

        var newPack = new Pack
        {
            Name = command.PackName,
            IsActive = command.Active,
            Price = command.Price
        };

        var resultAddContents = await TryAddContentsToPack(command.Content, newPack, cancellationToken);
        if(resultAddContents.IsT1)
            return resultAddContents.AsT1;

        var resultLinkChildPacks = await TryLinkChildPacks(command.ChildPackIds, newPack, cancellationToken);
        if(resultLinkChildPacks.IsT1)
            return resultLinkChildPacks.AsT1;

        var addedPack = await _packRepository.AddPackAsync(newPack, cancellationToken);

        return addedPack;
    }

    private async Task<OneOf<Success, PackValidationError>> TryAddContentsToPack(IEnumerable<string> contentItemNames, Pack newPack, CancellationToken cancellationToken)
    {
        foreach (var contentItemName in contentItemNames)
        {
            // Check if the pack already contains an item with the same name (to avoid duplicates)
            if (await _packRepository.PackItemExistsByNameAsync(contentItemName, cancellationToken))
                return new PackValidationError($"A pack item with the name {contentItemName} already exists");

            var packItem = new PackItem
            {
                Name = contentItemName,
                OwnerPack = newPack
            };

            newPack.PackItems.Add(packItem);
        }
        return new Success();
    }

    private async Task<OneOf<Success, PackValidationError>> TryLinkChildPacks(IEnumerable<Guid> childPackIds , Pack newPack, CancellationToken cancellationToken)
    {
        foreach (var childPackId in childPackIds)
        {
            var childPack = await _packRepository.GetPackByIdAsync(childPackId, cancellationToken);
            
            if (childPack is null)
                return new PackValidationError($"Child pack with id {childPackId} not found");

            if(childPack.ParentPack is not null)
                return new PackValidationError($"Child pack with id {childPackId} already has a parent");

            newPack.ChildPacks.Add(childPack);
        }

        return new Success();
    }
}