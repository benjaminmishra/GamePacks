namespace GamePacks.Service.Models.Responses;

public class GetAllPacksResponse
{
    public required IEnumerable<string> AllPackNames { get; set; }
}