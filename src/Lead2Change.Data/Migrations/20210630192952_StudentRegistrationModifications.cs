using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class StudentRegistrationModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PSATTestDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PSATTestScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParentApartmentNumber",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentState",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupportNeeded",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PSATTestDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PSATTestScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentApartmentNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentState",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SupportNeeded",
                table: "Students");
        }
    }
}
