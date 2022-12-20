using Microsoft.EntityFrameworkCore.Migrations;

namespace TAApplication.Migrations
{
    public partial class AddUnid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unid",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unid",
                table: "AspNetUsers");
        }
    }
}
