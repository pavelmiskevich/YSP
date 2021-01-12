using Microsoft.EntityFrameworkCore.Migrations;

namespace YSP.Data.Migrations
{
    public partial class InitialModelNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeOut",
                table: "Sites",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeOut",
                table: "Sites",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldDefaultValueSql: "((1))");
        }
    }
}
