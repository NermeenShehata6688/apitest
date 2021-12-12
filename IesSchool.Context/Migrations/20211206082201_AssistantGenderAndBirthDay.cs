using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IesSchool.Context.Migrations
{
    public partial class AssistantGenderAndBirthDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDay",
                table: "Assistant",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Assistant",
                type: "bit",
                nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_BirthCountry",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_FatherNationality",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_MotherNationality",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Nationality",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teacher",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAttachment_AttachmentType",
                table: "UserAttachment");

            migrationBuilder.DropIndex(
                name: "IX_UserAttachment_AttachmentTypeId",
                table: "UserAttachment");

            migrationBuilder.DropIndex(
                name: "IX_Students_BirthCountryId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FatherNationalityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_MotherNationalityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_NationalityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TeacherId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "Assistant");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Assistant");
        }
    }
}
