using Microsoft.EntityFrameworkCore.Migrations;

namespace YSP.Data.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "Queryes_Schedule_FK",
                table: "Schedules",
                newName: "Queries_Schedule_FK");

            migrationBuilder.RenameIndex(
                name: "IDX_Queryes_Id_IsActive",
                table: "Queries",
                newName: "IDX_Queries_Id_IsActive");

            migrationBuilder.RenameIndex(
                name: "Sites_Queryes_FK",
                table: "Queries",
                newName: "Sites_Queries_FK");

            migrationBuilder.RenameIndex(
                name: "Queryes_Positions_FK",
                table: "Positions",
                newName: "Queries_Positions_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "Queries_Schedule_FK",
                table: "Schedules",
                newName: "Queryes_Schedule_FK");

            migrationBuilder.RenameIndex(
                name: "IDX_Queries_Id_IsActive",
                table: "Queries",
                newName: "IDX_Queryes_Id_IsActive");

            migrationBuilder.RenameIndex(
                name: "Sites_Queries_FK",
                table: "Queries",
                newName: "Sites_Queryes_FK");

            migrationBuilder.RenameIndex(
                name: "Queries_Positions_FK",
                table: "Positions",
                newName: "Queryes_Positions_FK");
        }
    }
}
