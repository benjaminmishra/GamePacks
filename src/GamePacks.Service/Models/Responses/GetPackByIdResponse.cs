namespace GamePacks.Service.Models;

public class GetPackByIdResponse
{
    public required Guid Id {get;set;}
    public required string PackName { get; set; }
    public bool Active {get;set;}
    public int Price {get;set;}
    public IEnumerable<string> Content {get; set;} = new List<string> { };
    public IEnumerable<string> ChildPackIds {get; set;} = new List<string>();
}