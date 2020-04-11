using Microsoft.EntityFrameworkCore.Migrations;

namespace TourWebApp.Migrations
{
    public partial class newLocationSetConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationSets_LocationSetID",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "LocationSetID",
                table: "Locations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationSets_LocationSetID",
                table: "Locations",
                column: "LocationSetID",
                principalTable: "LocationSets",
                principalColumn: "LocationSetID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_LocationSets_LocationSetID",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "LocationSetID",
                table: "Locations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_LocationSets_LocationSetID",
                table: "Locations",
                column: "LocationSetID",
                principalTable: "LocationSets",
                principalColumn: "LocationSetID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
