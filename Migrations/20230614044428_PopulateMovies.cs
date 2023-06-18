using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidly.Migrations
{
    public partial class PopulateMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Movies (Id, Name, DateAdded, GenreId, NumberInStock, ReleaseDate)  Values (1,'Hangover', '2015-12-06', 1, 3, '2009-05-06')");
            migrationBuilder.Sql("INSERT INTO Movies (Id, Name, DateAdded, GenreId, NumberInStock, ReleaseDate)  Values (2,'Die Hard', '2017-10-11', 2, 10, '2005-05-26')");
            migrationBuilder.Sql("INSERT INTO Movies (Id, Name, DateAdded, GenreId, NumberInStock, ReleaseDate)  Values (3,'The Terminator', '2010-10-10', 2, 6,'2003-07-01')");
            migrationBuilder.Sql("INSERT INTO Movies (Id, Name, DateAdded, GenreId, NumberInStock, ReleaseDate)  Values (4,'Toy Story', '2013-12-12', 3, 7, '2008-10-08')");
            migrationBuilder.Sql("INSERT INTO Movies (Id, Name, DateAdded, GenreId, NumberInStock, ReleaseDate)  Values (5,'Titanic', '2008-12-08', 4, 14, '2007-05-10')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
