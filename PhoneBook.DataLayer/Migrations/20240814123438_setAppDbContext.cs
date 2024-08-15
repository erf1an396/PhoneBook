using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class setAppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_Contacts_ContactId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_Contacts_ContactId",
                table: "PhoneNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Email",
                table: "Email");

            migrationBuilder.RenameTable(
                name: "PhoneNumber",
                newName: "PhoneNumbers");

            migrationBuilder.RenameTable(
                name: "Email",
                newName: "Emails");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_ContactId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Email_ContactId",
                table: "Emails",
                newName: "IX_Emails_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emails",
                table: "Emails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Contacts_ContactId",
                table: "Emails",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Contacts_ContactId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Contacts_ContactId",
                table: "PhoneNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhoneNumbers",
                table: "PhoneNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emails",
                table: "Emails");

            migrationBuilder.RenameTable(
                name: "PhoneNumbers",
                newName: "PhoneNumber");

            migrationBuilder.RenameTable(
                name: "Emails",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_ContactId",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_ContactId",
                table: "Email",
                newName: "IX_Email_ContactId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhoneNumber",
                table: "PhoneNumber",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Email",
                table: "Email",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Contacts_ContactId",
                table: "Email",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_Contacts_ContactId",
                table: "PhoneNumber",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
