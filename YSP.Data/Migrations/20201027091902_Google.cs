using Microsoft.EntityFrameworkCore.Migrations;

namespace YSP.Data.Migrations
{
    public partial class Google : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Limit",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "GoogleCx",
                table: "Users",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleKey",
                table: "Users",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GoogleLimit",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YandexLimit",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleCx",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoogleKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoogleLimit",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "YandexLimit",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Limit",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
