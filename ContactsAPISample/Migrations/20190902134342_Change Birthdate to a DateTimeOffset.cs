using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsAPISample.Migrations
{
    public partial class ChangeBirthdatetoaDateTimeOffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Birthdate",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(DateTime));


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
