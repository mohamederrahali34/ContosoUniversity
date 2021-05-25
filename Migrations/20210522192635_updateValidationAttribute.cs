using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class updateValidationAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stage_Enseignant_EnseignantID",
                table: "Stage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enseignant",
                table: "Enseignant");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Enseignant");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Enseignant");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Enseignant");

            migrationBuilder.AlterColumn<int>(
                name: "SignatureValidation",
                table: "Stage",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "EnseignantID",
                table: "Enseignant",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enseignant",
                table: "Enseignant",
                column: "EnseignantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stage_Enseignant_EnseignantID",
                table: "Stage",
                column: "EnseignantID",
                principalTable: "Enseignant",
                principalColumn: "EnseignantID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stage_Enseignant_EnseignantID",
                table: "Stage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enseignant",
                table: "Enseignant");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "SignatureValidation",
                table: "Stage",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EnseignantID",
                table: "Enseignant",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Enseignant",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Enseignant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Enseignant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enseignant",
                table: "Enseignant",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stage_Enseignant_EnseignantID",
                table: "Stage",
                column: "EnseignantID",
                principalTable: "Enseignant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
