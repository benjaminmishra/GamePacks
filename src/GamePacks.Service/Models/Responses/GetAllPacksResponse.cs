namespace GamePacks.Service.Models;

public class GetAllPacksResponse
{
    public required IEnumerable<string> AllPackNames {get;set;}
}