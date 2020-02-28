using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MyMoviesMvc.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Director = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "Title", "Year" },
                values: new object[,]
                {
                    { -10, "Frank Darabont", "The Shawshank redemption", 1994 },
                    { -9, "Francis Ford Coppola", "The Godfather", 1972 },
                    { -8, "Christopher Nolan", "The Dark Knight", 2008 },
                    { -7, "Francis Ford Coppola", "The Godfather: Part II", 1974 },
                    { -6, "Peter Jackson", "The Lord of the Rings: The Return of the King", 2003 },
                    { -5, "Quentin Tarantino", "Pulp Fiction", 1994 },
                    { -4, "Steven Spielberg", "Schindler's List", 1993 },
                    { -3, "Sidney Lumet", "12 Angry Men", 1957 },
                    { -2, "Christopher Nolan", "Inception", 2010 },
                    { -1, "David Fincher", "Fight Club", 1999 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
