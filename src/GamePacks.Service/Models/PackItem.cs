using System;

namespace GamePacks.Service.Models;

public class PackItem
{
    public Guid Id { get; set;}
    public required string Name { get; set;}
    public required string ShortName { get; set;}
}
