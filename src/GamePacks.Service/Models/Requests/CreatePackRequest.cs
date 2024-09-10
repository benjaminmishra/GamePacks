namespace GamePacks.Service.Models.Requests;

public class CreatePackRequest
{
    public required string PackName { get; set; }
    public bool Active { get; set; }
    public int Price { get; set; }
    public IEnumerable<string> Content { get; set; } = [];
    public IEnumerable<Guid> ChildPackIds { get; set; } = [];
}