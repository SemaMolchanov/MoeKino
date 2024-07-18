using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoeKinoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateMoviesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    TitleRu = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    DescriptionRu = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    TrailerLinkEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TrailerLinkRu = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
