using Microsoft.EntityFrameworkCore.Migrations;

namespace MarathonBot.Src.SantaBot.Db.MigrationsData
{
    public partial class Add_ChosenByOtherCount_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChosenByOthersCount",
                schema: "santa",
                table: "UsersInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenByOthersCount",
                schema: "santa",
                table: "UsersInfo");
        }
    }
}
