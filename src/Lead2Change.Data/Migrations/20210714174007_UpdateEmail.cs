using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class UpdateEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OldParentEmail",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldStudentEmail",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldParentEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OldStudentEmail",
                table: "Students");
        }
    }
}
