using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB_Test.Migrations
{
    public partial class DefAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PwHash", "Role" },
                values: new object[] { 2, "Admin", "AQAAAAEAACcQAAAAEPRM8fK/AI7L9slRsEf1IiPXEhHdjRLtmHwQ2DyIyvuDOoaO8yyW8298r9mDuQVuqw==", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
