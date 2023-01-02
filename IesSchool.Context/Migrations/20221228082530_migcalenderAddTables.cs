using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IesSchool.Context.Migrations
{
    public partial class migcalenderAddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
           

            migrationBuilder.CreateTable(
                name: "CalenderCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalenderCategories", x => x.Id);
                });
          
            migrationBuilder.CreateTable(
                name: "Calenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CalenderCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calenders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calenders_CalenderCategories_CalenderCategoryId",
                        column: x => x.CalenderCategoryId,
                        principalTable: "CalenderCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calenders_CalenderCategoryId",
                table: "Calenders",
                column: "CalenderCategoryId");
            migrationBuilder.CreateIndex(
                name: "IX_User_Assistant_UserId",
                table: "User_Assistant",
                column: "UserId");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ApplicationGroupRole");

            migrationBuilder.DropTable(
                name: "ApplicationUserGroup");

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
                name: "Calenders");

            migrationBuilder.DropTable(
                name: "Event_Teacher");

            migrationBuilder.DropTable(
                name: "EventAttachmentBinary");

            migrationBuilder.DropTable(
                name: "EventStudentFileBinary");

            migrationBuilder.DropTable(
                name: "IepAssistant");

            migrationBuilder.DropTable(
                name: "ITP_GoalObjectiveActivity");

            migrationBuilder.DropTable(
                name: "ITP_ObjectiveProgressReport");

            migrationBuilder.DropTable(
                name: "ITP_Strategy");

            migrationBuilder.DropTable(
                name: "IXP_ExtraCurricular");

            migrationBuilder.DropTable(
                name: "LogComment");

            migrationBuilder.DropTable(
                name: "Objective_EvaluationProcess");

            migrationBuilder.DropTable(
                name: "Objective_Skill");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "ProgressReportExtraCurricular");

            migrationBuilder.DropTable(
                name: "ProgressReportParamedical");

            migrationBuilder.DropTable(
                name: "ProgressReportStrand");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Skill_AlowedDepartment");

            migrationBuilder.DropTable(
                name: "Student_ExtraTeacher");

            migrationBuilder.DropTable(
                name: "Student_Therapist");

            migrationBuilder.DropTable(
                name: "StudentAttachmentBinary");

            migrationBuilder.DropTable(
                name: "StudentHistoricalSkills");

            migrationBuilder.DropTable(
                name: "TherapistParamedicalService");

            migrationBuilder.DropTable(
                name: "User_Assistant");

            migrationBuilder.DropTable(
                name: "User_ExtraCurricular");

            migrationBuilder.DropTable(
                name: "UserAttachmentBinary");

            migrationBuilder.DropTable(
                name: "WorkCategory");

            migrationBuilder.DropTable(
                name: "ApplicationGroup");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CalenderCategories");

            migrationBuilder.DropTable(
                name: "EventAttachement");

            migrationBuilder.DropTable(
                name: "EventStudentFiles");

            migrationBuilder.DropTable(
                name: "ITP_GoalObjective");

            migrationBuilder.DropTable(
                name: "ITP_ProgressReport");

            migrationBuilder.DropTable(
                name: "Paramedical_Strategy");

            migrationBuilder.DropTable(
                name: "IXP");

            migrationBuilder.DropTable(
                name: "SkillEvaluation");

            migrationBuilder.DropTable(
                name: "Objective");

            migrationBuilder.DropTable(
                name: "IepProgressReport");

            migrationBuilder.DropTable(
                name: "StudentAttachment");

            migrationBuilder.DropTable(
                name: "Assistant");

            migrationBuilder.DropTable(
                name: "UserAttachment");

            migrationBuilder.DropTable(
                name: "Event_Student");

            migrationBuilder.DropTable(
                name: "ITP_Goal");

            migrationBuilder.DropTable(
                name: "IEP_ExtraCurricular");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "AttachmentType");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "ITP");

            migrationBuilder.DropTable(
                name: "ExtraCurricular");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "IEP_ParamedicalService");

            migrationBuilder.DropTable(
                name: "Strand");

            migrationBuilder.DropTable(
                name: "IEP");

            migrationBuilder.DropTable(
                name: "ParamedicalService");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Religion");

            migrationBuilder.DropTable(
                name: "AcadmicYears");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
