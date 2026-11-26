using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Movies.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NationalityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_People_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "NationalityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DirectorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_People_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MovieActors",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActors", x => new { x.MovieId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_MovieActors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieActors_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Nationalities",
                columns: new[] { "NationalityId", "Title" },
                values: new object[,]
                {
                    { 1, "British" },
                    { 2, "French" },
                    { 3, "American" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "Birthday", "FirstName", "LastName", "NationalityId" },
                values: new object[,]
                {
                    { 1, new DateTime(1970, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simon", "Pegg", 1 },
                    { 2, new DateTime(1976, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Benedict", "Cumberbatch", 1 },
                    { 3, new DateTime(1948, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jean", "Reno", 2 },
                    { 4, new DateTime(1980, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chris", "Pine", 3 },
                    { 5, new DateTime(1966, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "J. J.", "Abrams", 3 }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "DirectorId", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2015, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Force Awakens" },
                    { 2, 5, new DateTime(2009, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Trek" }
                });

            migrationBuilder.InsertData(
                table: "MovieActors",
                columns: new[] { "MovieId", "PersonId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_PersonId",
                table: "MovieActors",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_People_NationalityId",
                table: "People",
                column: "NationalityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieActors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Nationalities");
        }
    }
}
