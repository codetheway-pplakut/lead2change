using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class SetupBaseIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Students_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AspNetUserClaims",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AspNetRoleClaims",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
