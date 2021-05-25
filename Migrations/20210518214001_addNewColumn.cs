using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class addNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StageID",
                table: "Student",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Course",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Soutenance",
                columns: table => new
                {
                    SoutenanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<int>(nullable: false),
                    Jour = table.Column<DateTime>(nullable: false),
                    HeureDebut = table.Column<DateTime>(nullable: false),
                    HeureFin = table.Column<DateTime>(nullable: false),
                    Salle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soutenance", x => x.SoutenanceID);
                });

            migrationBuilder.CreateTable(
                name: "Enseignant",
                columns: table => new
                {
                    EnseignantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    DepartmentID = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: true),
                    SoutenanceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignant", x => x.EnseignantID);
                    table.ForeignKey(
                        name: "FK_Enseignant_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enseignant_Soutenance_SoutenanceID",
                        column: x => x.SoutenanceID,
                        principalTable: "Soutenance",
                        principalColumn: "SoutenanceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    StageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnBinome = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    OrganismeAceuil = table.Column<string>(maxLength: 50, nullable: true),
                    Pays = table.Column<string>(maxLength: 50, nullable: true),
                    Ville = table.Column<string>(maxLength: 50, nullable: true),
                    SignatureValidation = table.Column<bool>(nullable: false),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false),
                    EncadrantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.StageID);
                    table.ForeignKey(
                        name: "FK_Stage_Enseignant_EncadrantID",
                        column: x => x.EncadrantID,
                        principalTable: "Enseignant",
                        principalColumn: "EnseignantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_StageID",
                table: "Student",
                column: "StageID");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignant_DepartmentID",
                table: "Enseignant",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Enseignant_SoutenanceID",
                table: "Enseignant",
                column: "SoutenanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Stage_EncadrantID",
                table: "Stage",
                column: "EncadrantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Stage_StageID",
                table: "Student",
                column: "StageID",
                principalTable: "Stage",
                principalColumn: "StageID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Stage_StageID",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropTable(
                name: "Enseignant");

            migrationBuilder.DropTable(
                name: "Soutenance");

            migrationBuilder.DropIndex(
                name: "IX_Student_StageID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StageID",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Course",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
