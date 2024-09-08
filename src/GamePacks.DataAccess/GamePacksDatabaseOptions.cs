namespace GamePacks.Service;

public class GamePacksDatabaseOptions
{
    public const string ConfigSection = "Database";

    public required string ConnectionString{get;set;}
}