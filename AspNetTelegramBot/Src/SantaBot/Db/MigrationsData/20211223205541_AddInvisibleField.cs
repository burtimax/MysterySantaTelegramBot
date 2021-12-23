using Microsoft.EntityFrameworkCore.Migrations;

namespace MarathonBot.Src.SantaBot.Db.MigrationsData
{
    public partial class AddInvisibleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Invisible",
                schema: "santa",
                table: "UsersInfo",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Invisible",
                schema: "santa",
                table: "UsersInfo");
        }
    }
}
