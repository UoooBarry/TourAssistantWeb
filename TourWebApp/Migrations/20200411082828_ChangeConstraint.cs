using Microsoft.EntityFrameworkCore.Migrations;

namespace TourWebApp.Migrations
{
    public partial class ChangeConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Logins_LoginID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LoginID",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserID",
                table: "Logins",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Users_UserID",
                table: "Logins",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Users_UserID",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_UserID",
                table: "Logins");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LoginID",
                table: "Users",
                column: "LoginID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Logins_LoginID",
                table: "Users",
                column: "LoginID",
                principalTable: "Logins",
                principalColumn: "LoginID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
