using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePacks.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumsn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "PackItems");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Packs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Price",
                table: "Packs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Packs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PackItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
