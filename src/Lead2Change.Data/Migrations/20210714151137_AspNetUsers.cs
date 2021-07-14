using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lead2Change.Data.Migrations
{
    public partial class AspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<long>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<long>(nullable: false),
                    TwoFactorEnabled = table.Column<long>(nullable: false),
                    LockoutEnd = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<long>(nullable: false),
                    AccessFailedCount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CareerDeclarationId = table.Column<Guid>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false),
                    StudentFirstName = table.Column<string>(nullable: true),
                    StudentLastName = table.Column<string>(nullable: true),
                    StudentDateOfBirth = table.Column<DateTime>(nullable: false),
                    StudentAddress = table.Column<string>(nullable: true),
                    StudentApartmentNumber = table.Column<string>(nullable: true),
                    StudentCity = table.Column<string>(nullable: true),
                    StudentState = table.Column<string>(nullable: true),
                    StudentZipCode = table.Column<string>(nullable: true),
                    StudentHomePhone = table.Column<string>(nullable: true),
                    StudentCellPhone = table.Column<string>(nullable: true),
                    StudentEmail = table.Column<string>(nullable: true),
                    StudentCareerPath = table.Column<string>(nullable: true),
                    StudentCareerInterest = table.Column<string>(nullable: true),
                    ParentFirstName = table.Column<string>(nullable: true),
                    ParentLastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ParentApartmentNumber = table.Column<string>(nullable: true),
                    ParentCity = table.Column<string>(nullable: true),
                    ParentState = table.Column<string>(nullable: true),
                    ParentZipCode = table.Column<string>(nullable: true),
                    ParentHomePhone = table.Column<string>(nullable: true),
                    ParentCellPhone = table.Column<string>(nullable: true),
                    ParentEmail = table.Column<string>(nullable: true),
                    KnowGuidanceCounselor = table.Column<bool>(nullable: false),
                    GuidanceCounselorName = table.Column<string>(nullable: true),
                    MeetWithGuidanceCounselor = table.Column<bool>(nullable: false),
                    HowOftenMeetWithGuidanceCounselor = table.Column<string>(nullable: true),
                    DiscussWithGuidanceCounselor = table.Column<string>(nullable: true),
                    PlanAfterHighSchool = table.Column<string>(nullable: true),
                    CollegeApplicationStatus = table.Column<bool>(nullable: false),
                    CollegesList = table.Column<string>(nullable: true),
                    CollegeEssayStatus = table.Column<bool>(nullable: false),
                    CollegeEssayHelp = table.Column<bool>(nullable: false),
                    FirstChoiceCollege = table.Column<string>(nullable: true),
                    SecondChoiceCollege = table.Column<string>(nullable: true),
                    ThirdChoiceCollege = table.Column<string>(nullable: true),
                    TradeSchoolStatus = table.Column<bool>(nullable: false),
                    TradeSchoolsList = table.Column<string>(nullable: true),
                    ArmedForcesStatus = table.Column<bool>(nullable: false),
                    ArmedForcesBranch = table.Column<string>(nullable: true),
                    WorkStatus = table.Column<bool>(nullable: false),
                    CareerPathList = table.Column<string>(nullable: true),
                    OtherPlans = table.Column<string>(nullable: true),
                    PACTTestDate = table.Column<DateTime>(nullable: false),
                    PACTTestScore = table.Column<int>(nullable: false),
                    PSATTestDate = table.Column<DateTime>(nullable: false),
                    PSATTestScore = table.Column<int>(nullable: false),
                    SATTestDate = table.Column<DateTime>(nullable: false),
                    SATTestScore = table.Column<int>(nullable: false),
                    ACTTestDate = table.Column<DateTime>(nullable: false),
                    ACTTestScore = table.Column<int>(nullable: false),
                    PrepClassRequired = table.Column<bool>(nullable: false),
                    AssistanceForForms = table.Column<bool>(nullable: false),
                    FinancialAidProcessComplete = table.Column<bool>(nullable: false),
                    SupportNeeded = table.Column<string>(nullable: true),
                    StudentSignature = table.Column<string>(nullable: true),
                    StudentSignatureDate = table.Column<DateTime>(nullable: false),
                    ParentSignature = table.Column<string>(nullable: true),
                    ParentSignatureDate = table.Column<DateTime>(nullable: false),
                    CareerDeclarationId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_CareerDeclarations_CareerDeclarationId1",
                        column: x => x.CareerDeclarationId1,
                        principalTable: "CareerDeclarations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_StudentId",
                table: "Goals",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CareerDeclarationId1",
                table: "Students",
                column: "CareerDeclarationId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEvents");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CareerDeclarations");
        }
    }
}
