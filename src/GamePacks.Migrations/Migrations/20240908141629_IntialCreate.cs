using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePacks.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ParentPackId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packs_Packs_ParentPackId",
                        column: x => x.ParentPackId,
                        principalTable: "Packs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    OwnerPackId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackItems_Packs_OwnerPackId",
                        column: x => x.OwnerPackId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackItems_OwnerPackId",
                table: "PackItems",
                column: "OwnerPackId");

            migrationBuilder.CreateIndex(
                name: "IX_Packs_ParentPackId",
                table: "Packs",
                column: "ParentPackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackItems");

            migrationBuilder.DropTable(
                name: "Packs");
        }
    }
}
