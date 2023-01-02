using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IesSchool.Context.Migrations
{
    public partial class migcalender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Area_Program",
                table: "Area");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Program",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_IEP_ExtraCurricular_User",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropForeignKey(
                name: "FK_IEP_ParamedicalService_Therapist",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parent",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Religion",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Term_AcadmicYears_AcadmicYearId",
                table: "Term");

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
                name: "Program");

            migrationBuilder.DropTable(
                name: "ProgressReportExtraCurricular");

            migrationBuilder.DropTable(
                name: "ProgressReportParamedical");

            migrationBuilder.DropTable(
                name: "ProgressReportStrand");

            migrationBuilder.DropTable(
                name: "Student_ExtraTeacher");

            migrationBuilder.DropTable(
                name: "User_ExtraCurricular");

            migrationBuilder.DropTable(
                name: "ITP_GoalObjective");

            migrationBuilder.DropTable(
                name: "ITP_ProgressReport");

            migrationBuilder.DropTable(
                name: "Paramedical_Strategy");

            migrationBuilder.DropTable(
                name: "IXP");

            migrationBuilder.DropTable(
                name: "IepProgressReport");

            migrationBuilder.DropTable(
                name: "ITP_Goal");

            migrationBuilder.DropTable(
                name: "ITP");

            migrationBuilder.DropIndex(
                name: "IX_Students_ParentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ReligionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_IEP_ParamedicalService_TherapistId",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropIndex(
                name: "IX_IEP_ExtraCurricular_ExTeacherId",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropIndex(
                name: "IX_Goals_ProgramId",
                table: "Goals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Area_ProgramId",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "DeviceToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ParentCivilId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SkillNumber",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "Setting");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropColumn(
                name: "IsItpCreated",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropColumn(
                name: "TherapistId",
                table: "IEP_ParamedicalService");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropColumn(
                name: "ExTeacherId",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropColumn(
                name: "IsIxpCreated",
                table: "IEP_ExtraCurricular");

            migrationBuilder.DropColumn(
                name: "LongTermNumber",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EventType");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "EventType");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "EventType");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "EventType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EventType");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "code",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Area");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Term",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TermType",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PassportNumber",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CivilId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HeadOfEducation",
                table: "IEP",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Term_AcadmicYears",
                table: "Term",
                column: "AcadmicYearId",
                principalTable: "AcadmicYears",
                principalColumn: "Id");
        }
    }
}
