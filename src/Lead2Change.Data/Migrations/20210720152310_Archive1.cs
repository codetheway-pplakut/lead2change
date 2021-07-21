using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class Archive1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "OldParentEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OldStudentEmail",
                table: "Students");            

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Questions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Answers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "OldParentEmail",
                table: "Students",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldStudentEmail",
                table: "Students",
                type: "TEXT",
                nullable: true);
        }
    }
}
