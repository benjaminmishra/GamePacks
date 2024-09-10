using GamePacks.DataAccess.Models;
using GamePacks.Service.Models.Responses;

namespace GamePacks.Service.Endpoints;

public static class GamePackResponseMapper
{
    public static GetPackByIdResponse MapPackToResponse(this Pack pack)
    {
        return new GetPackByIdResponse
        {
            Id = pack.Id,
            PackName = pack.Name,
            Active = pack.IsActive,
            Price = pack.Price,
            Content = pack.PackItems.Select(MapPackItemToResponse).ToList(),
            ChildPacks = pack.ChildPacks.Select(MapPackToResponse).ToList()
        };
    }

    private static PackItemResponse MapPackItemToResponse(PackItem packItem)
    {
        return new PackItemResponse
        {
            Id = packItem.Id,
            Name = packItem.Name
        };
    }
}
