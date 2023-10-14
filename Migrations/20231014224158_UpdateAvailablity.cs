using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidly.Migrations
{
    public partial class UpdateAvailablity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql("UPDATE Movies SET NumberAvailable = NumberInStock;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
