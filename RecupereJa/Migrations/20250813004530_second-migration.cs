using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecupereJa.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Items");

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoUsuario",
                table: "Usuarios",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagemObjeto",
                table: "Items",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoUsuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ImagemObjeto",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Items",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
