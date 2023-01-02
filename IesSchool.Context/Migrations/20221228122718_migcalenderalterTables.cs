using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IesSchool.Context.Migrations
{
    public partial class migcalenderalterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calenders_CalenderCategories_CalenderCategoryId",
                table: "Calenders");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Calenders",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Calenders",
                newName: "End");

            migrationBuilder.AlterColumn<int>(
                name: "CalenderCategoryId",
                table: "Calenders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Calenders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calenders_CalenderCategories_CalenderCategoryId",
                table: "Calenders",
                column: "CalenderCategoryId",
                principalTable: "CalenderCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calenders_CalenderCategories_CalenderCategoryId",
                table: "Calenders");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Calenders");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Calenders",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Calenders",
                newName: "EndDate");

            migrationBuilder.AlterColumn<int>(
                name: "CalenderCategoryId",
                table: "Calenders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Calenders_CalenderCategories_CalenderCategoryId",
                table: "Calenders",
                column: "CalenderCategoryId",
                principalTable: "CalenderCategories",
                principalColumn: "Id");
        }
    }
}
