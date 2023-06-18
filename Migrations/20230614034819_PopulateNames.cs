using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidly.Migrations
{
    public partial class PopulateNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Pay as You Go' Where Id=1");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Monthly' Where Id=2");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Quarterly' Where Id=3");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Yearly' Where Id=4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
