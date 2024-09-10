using System;

namespace GamePacks.DataAccess.Models;

public class PackItem
{
    public Guid Id {get; set;}
    public required string Name {get; set;}
    public Pack? OwnerPack {get;set;}
}
