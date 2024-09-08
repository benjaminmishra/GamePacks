using GamePacks.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamePacks.DataAccess.EntityConfigurations;

public class PackItemEntityTypeConfiguration : IEntityTypeConfiguration<PackItem>
{
    public void Configure(EntityTypeBuilder<PackItem> builder)
    {
        builder.ToTable("PackItems");

        builder
        .HasKey(x => x.Id);

        builder
        .Property(x => x.Id)
        .ValueGeneratedOnAdd();

        builder
        .Property(x => x.Name)
        .IsRequired();
 
        builder
        .HasOne(x => x.OwnerPack)
        .WithMany(x => x.PackItems);
    }
}