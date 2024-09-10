namespace GamePacks.Service.Models;

public class GetPackByIdResponse
{
    public required Guid Id {get;set;}
    public required string PackName { get; set; }
    public bool Active {get;set;}
    public int Price {get;set;}
    public IEnumerable<PackItemResponse> Content {get; set;} = [];
    public IEnumerable<GetPackByIdResponse> ChildPacks {get; set;} = [];
}

public class PackItemResponse
{
    public Guid Id {get;set;}
    public required string Name {get;set;}
}
