using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemesterYear_Assignment3_caenders.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieAndActorModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Birthdate = table.Column<DateOnly>(type: "date", nullable: false),
                    Media_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMDB_URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Release_year = table.Column<long>(type: "bigint", nullable: false),
                    IMDB_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Media_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ActorModelMovieModel",
                columns: table => new
                {
                    ActorsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoviesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorModelMovieModel", x => new { x.ActorsID, x.MoviesID });
                    table.ForeignKey(
                        name: "FK_ActorModelMovieModel_Actors_ActorsID",
                        column: x => x.ActorsID,
                        principalTable: "Actors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorModelMovieModel_Movies_MoviesID",
                        column: x => x.MoviesID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorModelMovieModel_MoviesID",
                table: "ActorModelMovieModel",
                column: "MoviesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorModelMovieModel");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
