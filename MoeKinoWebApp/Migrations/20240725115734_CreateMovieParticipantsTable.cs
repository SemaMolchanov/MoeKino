using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoeKinoWebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateMovieParticipantsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieParticipants",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    MovieParticipantCategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieParticipants", x => new { x.MovieID, x.PersonID, x.MovieParticipantCategoryID });
                    table.ForeignKey(
                        name: "FK_MovieParticipants_MovieParticipantCategories_MovieParticipantCategoryID",
                        column: x => x.MovieParticipantCategoryID,
                        principalTable: "MovieParticipantCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieParticipants_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieParticipants_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieParticipants_MovieParticipantCategoryID",
                table: "MovieParticipants",
                column: "MovieParticipantCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieParticipants_PersonID",
                table: "MovieParticipants",
                column: "PersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieParticipants");
        }
    }
}
