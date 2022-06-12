using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Test.Migrations
{
    public partial class NameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tests_Name",
                table: "Tests",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_SpecialtiesName",
                table: "Specialties",
                column: "SpecialtiesName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Institutes_InstitutesName",
                table: "Institutes",
                column: "InstitutesName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupName",
                table: "Groups",
                column: "GroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_DisciplineName",
                table: "Disciplines",
                column: "DisciplineName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tests_Name",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_SpecialtiesName",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Institutes_InstitutesName",
                table: "Institutes");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupName",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_DisciplineName",
                table: "Disciplines");
        }
    }
}
