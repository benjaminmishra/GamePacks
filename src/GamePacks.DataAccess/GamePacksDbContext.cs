using GamePacks.DataAccess.EntityConfigurations;
using GamePacks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePacks.DataAccess;

public class GamePacksDbContext : DbContext
{
    public DbSet<Pack> Packs { get; set; }
    public DbSet<PackItem> PackItems { get; set; }

    public GamePacksDbContext(DbContextOptions<GamePacksDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PackEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PackItemEntityTypeConfiguration());
    }
}
