using GamePacks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePacks.DataAccess.EntityConfigurations;

public class PackEntityTypeConfiguration : IEntityTypeConfiguration<Pack>
{
    public void Configure(EntityTypeBuilder<Pack> builder)
    {
        builder.ToTable("Packs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
        .ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.IsActive)
        .IsRequired()
        .HasDefaultValue(false);

        builder.Property(x => x.Price)
        .IsRequired()
        .HasDefaultValue(0);
        
        builder
        .HasOne(x => x.ParentPack)
        .WithMany(x=>x.ChildPacks);
    }
}