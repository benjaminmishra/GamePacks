﻿// <auto-generated />
using System;
using GamePacks.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GamePacks.Migrations.Migrations
{
    [DbContext(typeof(GamePacksDbContext))]
    [Migration("20240908141629_IntialCreate")]
    partial class IntialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GamePacks.DataAccess.Models.Pack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentPackId")
                        .HasColumnType("uuid");

                    b.Property<long>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentPackId");

                    b.ToTable("Packs", (string)null);
                });

            modelBuilder.Entity("GamePacks.DataAccess.Models.PackItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerPackId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerPackId");

                    b.ToTable("PackItems", (string)null);
                });

            modelBuilder.Entity("GamePacks.DataAccess.Models.Pack", b =>
                {
                    b.HasOne("GamePacks.DataAccess.Models.Pack", "ParentPack")
                        .WithMany("ChildPacks")
                        .HasForeignKey("ParentPackId");

                    b.Navigation("ParentPack");
                });

            modelBuilder.Entity("GamePacks.DataAccess.Models.PackItem", b =>
                {
                    b.HasOne("GamePacks.DataAccess.Models.Pack", "OwnerPack")
                        .WithMany("PackItems")
                        .HasForeignKey("OwnerPackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerPack");
                });

            modelBuilder.Entity("GamePacks.DataAccess.Models.Pack", b =>
                {
                    b.Navigation("ChildPacks");

                    b.Navigation("PackItems");
                });
#pragma warning restore 612, 618
        }
    }
}
