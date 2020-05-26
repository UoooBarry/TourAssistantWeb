using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourWebApp.Migrations
{
    public partial class AddMinDurationToTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "MinDuration",
                table: "Tours",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinDuration",
                table: "Tours");
        }
    }
}
