using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LexiconUniversity.Data.Migrations
{
    public partial class ChangedDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Course",
                newName: "Course Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "Edited",
                table: "Student",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edited",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "Course Name",
                table: "Course",
                newName: "Title");
        }
    }
}
