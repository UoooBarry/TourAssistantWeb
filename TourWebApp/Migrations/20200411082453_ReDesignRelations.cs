using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourWebApp.Migrations
{
    public partial class ReDesignRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationSets",
                columns: table => new
                {
                    LocationSetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSets", x => x.LocationSetID);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginID = table.Column<int>(maxLength: 8, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 64, nullable: false),
                    ActivationStatus = table.Column<bool>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginID);
                    table.CheckConstraint("CH_Login_LoginID", "len(LoginID) = 8");
                    table.CheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");
                });

            migrationBuilder.CreateTable(
                name: "TourTypes",
                columns: table => new
                {
                    TourTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTypes", x => x.TourTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    X = table.Column<float>(nullable: false),
                    Y = table.Column<float>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    MinTime = table.Column<TimeSpan>(nullable: false),
                    LocationSetID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_Locations_LocationSets_LocationSetID",
                        column: x => x.LocationSetID,
                        principalTable: "LocationSets",
                        principalColumn: "LocationSetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LoginID = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Logins_LoginID",
                        column: x => x.LoginID,
                        principalTable: "Logins",
                        principalColumn: "LoginID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    TourTypeID = table.Column<int>(nullable: false),
                    MinDuration = table.Column<TimeSpan>(nullable: false),
                    LocationSetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourID);
                    table.ForeignKey(
                        name: "FK_Tours_LocationSets_LocationSetID",
                        column: x => x.LocationSetID,
                        principalTable: "LocationSets",
                        principalColumn: "LocationSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tours_TourTypes_TourID",
                        column: x => x.TourID,
                        principalTable: "TourTypes",
                        principalColumn: "TourTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationSetID",
                table: "Locations",
                column: "LocationSetID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_LocationSetID",
                table: "Tours",
                column: "LocationSetID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LoginID",
                table: "Users",
                column: "LoginID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LocationSets");

            migrationBuilder.DropTable(
                name: "TourTypes");

            migrationBuilder.DropTable(
                name: "Logins");
        }
    }
}
