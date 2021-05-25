using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class v : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StageID",
                table: "Soutenance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Soutenance_StageID",
                table: "Soutenance",
                column: "StageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Soutenance_Stage_StageID",
                table: "Soutenance",
                column: "StageID",
                principalTable: "Stage",
                principalColumn: "StageID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soutenance_Stage_StageID",
                table: "Soutenance");

            migrationBuilder.DropIndex(
                name: "IX_Soutenance_StageID",
                table: "Soutenance");

            migrationBuilder.DropColumn(
                name: "StageID",
                table: "Soutenance");
        }
    }
}
