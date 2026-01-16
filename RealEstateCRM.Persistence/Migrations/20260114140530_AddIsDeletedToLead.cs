using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateCRM.Persistence.Migrations
{
    public partial class AddIsDeletedToLead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Leads");
        }
    }
}
