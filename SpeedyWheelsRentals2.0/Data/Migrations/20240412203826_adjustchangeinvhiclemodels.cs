using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpeedyWheelsRentals2._0.Data.Migrations
{
    public partial class adjustchangeinvhiclemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DailyRentalPrice",
                table: "Vehicle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyRentalPrice",
                table: "Vehicle");
        }
    }
}
