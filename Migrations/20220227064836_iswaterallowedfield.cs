using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterPlant.Migrations
{
    public partial class iswaterallowedfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isWaterAllowed",
                table: "Plants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isWaterAllowed",
                table: "Plants");
        }
    }
}
