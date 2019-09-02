using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsAPISample.Migrations
{
    public partial class Addemailaddressestocontacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailAddresses",
                columns: table => new
                {
                    ContactID = table.Column<long>(nullable: false),
                    Email = table.Column<string>(maxLength: 40, nullable: false),
                    Type = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddresses", x => x.Email);
                    table.ForeignKey(
                        name: "FK_EmailAddresses_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_ContactID",
                table: "EmailAddresses",
                column: "ContactID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAddresses");
        }
    }
}
