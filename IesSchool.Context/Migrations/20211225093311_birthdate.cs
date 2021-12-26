using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IesSchool.Context.Migrations
{
    public partial class birthdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "User",
                type: "datetime2",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Objective",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Student_Event",
                table: "Event_Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Teacher_Event",
                table: "Event_Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAttachement_Event",
                table: "EventAttachement");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAttachmentBinary_EventAttachement",
                table: "EventAttachmentBinary");

            migrationBuilder.DropForeignKey(
                name: "FK_EventStudentFileBinary_EventStudentFiles",
                table: "EventStudentFileBinary");

            migrationBuilder.DropForeignKey(
                name: "FK_EventStudentFiles_Event_Student1",
                table: "EventStudentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_Goals",
                table: "Objective");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_EvaluationProcess_Objective",
                table: "Objective_EvaluationProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_Objective_Skill_Objective",
                table: "Objective_Skill");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_City",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_State",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StateId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InactiveReason",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Entry",
                table: "Objective");

            migrationBuilder.DropColumn(
                name: "Evaluation",
                table: "Objective");

            migrationBuilder.DropColumn(
                name: "InstructionPractice",
                table: "Objective");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Assistant");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Assistant",
                type: "bool",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationGroupId",
                table: "ApplicationUserGroup",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationRoleId",
                table: "ApplicationGroupRole",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationGroupId",
                table: "ApplicationGroupRole",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ApplicationGroup",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Objective",
                table: "Activities",
                column: "ObjectiveId",
                principalTable: "Objective",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Student_Event",
                table: "Event_Student",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Teacher_Event",
                table: "Event_Teacher",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttachement_Event",
                table: "EventAttachement",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttachmentBinary_EventAttachement",
                table: "EventAttachmentBinary",
                column: "Id",
                principalTable: "EventAttachement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventStudentFileBinary_EventStudentFiles",
                table: "EventStudentFileBinary",
                column: "Id",
                principalTable: "EventStudentFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventStudentFiles_Event_Student1",
                table: "EventStudentFiles",
                column: "EventStudentId",
                principalTable: "Event_Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Objective_Goals",
                table: "Objective",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Objective_EvaluationProcess_Objective",
                table: "Objective_EvaluationProcess",
                column: "ObjectiveId",
                principalTable: "Objective",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Objective_Skill_Objective",
                table: "Objective_Skill",
                column: "ObjectiveId",
                principalTable: "Objective",
                principalColumn: "Id");
        }
    }
}
