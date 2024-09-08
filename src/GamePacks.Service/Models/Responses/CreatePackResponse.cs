namespace GamePacks.Service.Endpoints;

public class CreatePackResponse
{
    public string Id {get;set;} 
    public string PackName { get; set; }
    public bool PackIsActive {get;set;}
    public int Price {get;set;}
    public IEnumerable<string> ContentIds {get; set;} = new List<string> { };
    public IEnumerable<string> ChildPackIds {get; set;} = new List<string>();
}