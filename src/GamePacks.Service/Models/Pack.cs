using System;

namespace GamePacks.Service.Models;

public class Pack
{
    public Guid Id {get;set;}
    public required string Name {get;set;}
    public required string ShortName {get;set;}
    public long Price {get;set;}
    public bool IsActive {get;set;}
    public PackItem[]? PackItems {get;set;}
    public Pack? ParentPack {get;set;}
}
