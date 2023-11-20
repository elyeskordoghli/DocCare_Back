using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocCare_Backend.Migrations
{
    /// <inheritdoc />
    public partial class ADD_Patient_DossierMedical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Patient_PatientId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_DossiersMedicaux_Patient_PatientId",
                table: "DossiersMedicaux");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Patient_TempId",
                table: "Patient");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Patient_TempId1",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "TempId",
                table: "Patient");

            migrationBuilder.RenameTable(
                name: "Patient",
                newName: "Patients");

            migrationBuilder.RenameColumn(
                name: "TempId1",
                table: "Patients",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Patients",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DateN",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Num",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DossiersMedicaux_Patients_PatientId",
                table: "DossiersMedicaux",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_DossiersMedicaux_Patients_PatientId",
                table: "DossiersMedicaux");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DateN",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Num",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Patient");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patient",
                newName: "TempId1");

            migrationBuilder.AlterColumn<int>(
                name: "TempId1",
                table: "Patient",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Patient_TempId",
                table: "Patient",
                column: "TempId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Patient_TempId1",
                table: "Patient",
                column: "TempId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Patient_PatientId",
                table: "Consultations",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DossiersMedicaux_Patient_PatientId",
                table: "DossiersMedicaux",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "TempId1",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
