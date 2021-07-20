using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class InterviewAndQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
               migrationBuilder.DropColumn(
                name: "OldParentEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OldStudentEmail",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Interviews_InterviewId",
                table: "Questions",
                column: "InterviewId",
                principalTable: "Interviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
             */

            migrationBuilder.AddColumn<Guid>(
                name: "InterviewId",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_InterviewId",
                table: "Questions",
                column: "InterviewId");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Interviews_InterviewId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_InterviewId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "InterviewId",
                table: "Questions");

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
