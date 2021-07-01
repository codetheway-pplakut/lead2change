﻿// <auto-generated />
using System;
using Lead2Change.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lead2Change.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210630192952_StudentRegistrationModifications")]
    partial class StudentRegistrationModifications
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15");

            modelBuilder.Entity("Lead2Change.Domain.Models.AppEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.Property<string>("StackTrace")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AppEvents");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetRoleClaims", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetRoles", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserClaims", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserLogins", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserTokens", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<long>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<long>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<long>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<long>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.CareerDeclaration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CareerCluster")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CollegeBound")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecificCareer")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TechnicalCollegeBound")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("CareerDeclarations");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.Goal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateGoalSet")
                        .HasColumnType("TEXT");

                    b.Property<string>("Explanation")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("GoalReviewDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("GoalSet")
                        .HasColumnType("TEXT");

                    b.Property<string>("SEL")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WasItAccomplished")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ACTTestDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ACTTestScore")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Accepted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("ArmedForcesBranch")
                        .HasColumnType("TEXT");

                    b.Property<bool>("ArmedForcesStatus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AssistanceForForms")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CareerPathList")
                        .HasColumnType("TEXT");

                    b.Property<bool>("CollegeApplicationStatus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CollegeEssayHelp")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CollegeEssayStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CollegesList")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Declined")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DiscussWithGuidanceCounselor")
                        .HasColumnType("TEXT");

                    b.Property<bool>("FinancialAidProcessComplete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstChoiceCollege")
                        .HasColumnType("TEXT");

                    b.Property<string>("GuidanceCounselorName")
                        .HasColumnType("TEXT");

                    b.Property<string>("HowOftenMeetWithGuidanceCounselor")
                        .HasColumnType("TEXT");

                    b.Property<bool>("KnowGuidanceCounselor")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MeetWithGuidanceCounselor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OtherPlans")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PACTTestDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PACTTestScore")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PSATTestDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PSATTestScore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ParentApartmentNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentCellPhone")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentCity")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentFirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentHomePhone")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentLastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentSignature")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ParentSignatureDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentState")
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentZipCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlanAfterHighSchool")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PrepClassRequired")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SATTestDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SATTestScore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecondChoiceCollege")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentApartmentNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentCareerInterest")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentCareerPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentCellPhone")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentCity")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StudentDateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentFirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentHomePhone")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentLastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentSignature")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StudentSignatureDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentState")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentZipCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("SupportNeeded")
                        .HasColumnType("TEXT");

                    b.Property<string>("ThirdChoiceCollege")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TradeSchoolStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TradeSchoolsList")
                        .HasColumnType("TEXT");

                    b.Property<bool>("WorkStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetRoleClaims", b =>
                {
                    b.HasOne("Lead2Change.Domain.Models.AspNetRoles", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserClaims", b =>
                {
                    b.HasOne("Lead2Change.Domain.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserLogins", b =>
                {
                    b.HasOne("Lead2Change.Domain.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserRoles", b =>
                {
                    b.HasOne("Lead2Change.Domain.Models.AspNetRoles", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lead2Change.Domain.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.AspNetUserTokens", b =>
                {
                    b.HasOne("Lead2Change.Domain.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.CareerDeclaration", b =>
                {
                    b.HasOne("Lead2Change.Domain.Models.Student", null)
                        .WithOne("CareerDeclaration")
                        .HasForeignKey("Lead2Change.Domain.Models.CareerDeclaration", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lead2Change.Domain.Models.Goal", b =>
                {
                    b.HasOne("Lead2Change.Domain.Models.Student", null)
                        .WithMany("Goals")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
