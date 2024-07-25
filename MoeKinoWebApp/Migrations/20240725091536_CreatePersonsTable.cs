using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoeKinoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatePersonsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FullNameRu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortBioEn = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    ShortBioRu = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
