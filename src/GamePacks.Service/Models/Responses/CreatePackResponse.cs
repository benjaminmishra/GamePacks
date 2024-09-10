namespace GamePacks.Service.Models;

public class CreatePackResponse
{
    public Guid Id {get;set;}
    public required string PackName {get;set;}
    public bool Active {get;set;}
    public int Price {get;set;}
    public IEnumerable<string> Contents {get; set;} = [];
    public IEnumerable<Guid> ChildPackIds {get; set;} = [];
}
