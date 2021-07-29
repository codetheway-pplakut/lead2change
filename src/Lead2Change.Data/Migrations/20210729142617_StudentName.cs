using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class StudentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InterviewName",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterviewName",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Answers");
        }
    }
}
