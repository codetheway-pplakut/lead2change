using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ACTTestDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ACTTestScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArmedForcesBranch",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ArmedForcesStatus",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AssistanceForForms",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CareerPathList",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CollegeApplicationStatus",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CollegeEssayHelp",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CollegeEssayStatus",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CollegesList",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Declined",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DiscussWithGuidanceCounselor",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FinancialAidProcessComplete",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstChoiceCollege",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuidanceCounselorName",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowOftenMeetWithGuidanceCounselor",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "KnowGuidanceCounselor",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MeetWithGuidanceCounselor",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherPlans",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PACTTestDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PACTTestScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParentCellPhone",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentCity",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentEmail",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentFirstName",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentHomePhone",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentLastName",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentSignature",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ParentSignatureDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ParentState",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentZipCode",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlanAfterHighSchool",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrepClassRequired",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SATTestDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SATTestScore",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SecondChoiceCollege",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentAddress",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentApartmentNumber",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentCareerInterest",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentCareerPath",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentCellPhone",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentCity",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StudentDateOfBirth",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StudentEmail",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentFirstName",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentHomePhone",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentLastName",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentSignature",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StudentSignatureDate",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StudentZipCode",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdChoiceCollege",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TradeSchoolStatus",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TradeSchoolsList",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkStatus",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareerDeclarations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    CollegeBound = table.Column<bool>(nullable: false),
                    CareerCluster = table.Column<int>(nullable: false),
                    SpecificCareer = table.Column<string>(nullable: true),
                    TechnicalCollegeBound = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerDeclarations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerDeclarations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    GoalSet = table.Column<string>(nullable: true),
                    DateGoalSet = table.Column<DateTime>(nullable: false),
                    SEL = table.Column<string>(nullable: true),
                    GoalReviewDate = table.Column<DateTime>(nullable: false),
                    WasItAccomplished = table.Column<string>(nullable: true),
                    Explanation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareerDeclarations_StudentId",
                table: "CareerDeclarations",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_StudentId",
                table: "Goals",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEvents");

            migrationBuilder.DropTable(
                name: "CareerDeclarations");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropColumn(
                name: "ACTTestDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ACTTestScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ArmedForcesBranch",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ArmedForcesStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AssistanceForForms",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CareerPathList",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CollegeApplicationStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CollegeEssayHelp",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CollegeEssayStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CollegesList",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Declined",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DiscussWithGuidanceCounselor",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FinancialAidProcessComplete",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FirstChoiceCollege",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GuidanceCounselorName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HowOftenMeetWithGuidanceCounselor",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "KnowGuidanceCounselor",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MeetWithGuidanceCounselor",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OtherPlans",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PACTTestDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PACTTestScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentCellPhone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentCity",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentFirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentHomePhone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentLastName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentSignature",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentSignatureDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentState",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentZipCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PlanAfterHighSchool",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PrepClassRequired",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SATTestDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SATTestScore",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "SecondChoiceCollege",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentAddress",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentApartmentNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentCareerInterest",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentCareerPath",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentCellPhone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentCity",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentDateOfBirth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentFirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentHomePhone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentLastName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentSignature",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentSignatureDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentZipCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ThirdChoiceCollege",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TradeSchoolStatus",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TradeSchoolsList",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "WorkStatus",
                table: "Students");
        }
    }
}
