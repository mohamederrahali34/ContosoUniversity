using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class deleteColumnStageID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soutenance_Stage_StageID",
                table: "Soutenance");

            migrationBuilder.AlterColumn<int>(
                name: "StageID",
                table: "Soutenance",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Soutenance_Stage_StageID",
                table: "Soutenance",
                column: "StageID",
                principalTable: "Stage",
                principalColumn: "StageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soutenance_Stage_StageID",
                table: "Soutenance");

            migrationBuilder.AlterColumn<int>(
                name: "StageID",
                table: "Soutenance",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Soutenance_Stage_StageID",
                table: "Soutenance",
                column: "StageID",
                principalTable: "Stage",
                principalColumn: "StageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
