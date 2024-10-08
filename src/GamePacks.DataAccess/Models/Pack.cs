namespace GamePacks.DataAccess.Models;

public class Pack
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Price { get; set; }
    public bool IsActive { get; set; }
    public Pack? ParentPack { get; set; }
    public ICollection<PackItem> PackItems { get; set; } = [];
    public ICollection<Pack> ChildPacks { get; set; } = [];
}
