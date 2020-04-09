using Microsoft.EntityFrameworkCore.Migrations;

namespace SkillManagement.API.Data.Migrations
{
    public partial class InitialMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserSkillLevel",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserSkillLevel",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Description", "LevelName" },
                values: new object[,]
                {
                    { 1L, "Low", "1" },
                    { 2L, "Medium", "2" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Description", "SkillName" },
                values: new object[,]
                {
                    { 1L, ".Net", ".Net core" },
                    { 2L, ".Net", "Asp.Net core" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1L, "JDove@gmail.com", "John", "Dove" },
                    { 2L, "J1Dove1@gmail.com", "John1", "Dove1" }
                });

            migrationBuilder.InsertData(
                table: "UserSkillLevel",
                columns: new[] { "Id", "LevelId", "SkillId", "UserId" },
                values: new object[] { 1L, 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "UserSkillLevel",
                columns: new[] { "Id", "LevelId", "SkillId", "UserId" },
                values: new object[] { 2L, 2L, 2L, 1L });
        }
    }
}
