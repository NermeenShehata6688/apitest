using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IesSchool.Context.Migrations
{
    public partial class intial : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder) { }
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
                name: "City");

            migrationBuilder.DropTable(
                name: "Event_Teacher");

            migrationBuilder.DropTable(
                name: "EventAttachmentBinary");

            migrationBuilder.DropTable(
                name: "EventStudentFileBinary");

            migrationBuilder.DropTable(
                name: "IEP_ExtraCurricular");

            migrationBuilder.DropTable(
                name: "IEP_ParamedicalService");

            migrationBuilder.DropTable(
                name: "IepAssistant");

            migrationBuilder.DropTable(
                name: "Objective_EvaluationProcess");

            migrationBuilder.DropTable(
                name: "Objective_Skill");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Religion");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Skill_AlowedDepartment");

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
                name: "State");

            migrationBuilder.DropTable(
                name: "EventAttachement");

            migrationBuilder.DropTable(
                name: "EventStudentFiles");

            migrationBuilder.DropTable(
                name: "ExtraCurricular");

            migrationBuilder.DropTable(
                name: "SkillEvaluation");

            migrationBuilder.DropTable(
                name: "Objective");

            migrationBuilder.DropTable(
                name: "StudentAttachment");

            migrationBuilder.DropTable(
                name: "ParamedicalService");

            migrationBuilder.DropTable(
                name: "Assistant");

            migrationBuilder.DropTable(
                name: "UserAttachment");

            migrationBuilder.DropTable(
                name: "Event_Student");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "AttachmentType");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "IEP");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Strand");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "AcadmicYears");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
