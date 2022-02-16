using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSharpTraining.Migrations
{
    public partial class initials111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "CreateUserTabs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UniqueCode",
                table: "CreateUserTabs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "CreateUserTabs");

            migrationBuilder.DropColumn(
                name: "UniqueCode",
                table: "CreateUserTabs");
        }
    }
}
