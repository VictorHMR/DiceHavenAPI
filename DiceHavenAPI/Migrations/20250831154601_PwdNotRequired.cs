using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceHaven_API.Migrations
{
    /// <inheritdoc />
    public partial class PwdNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DS_SENHA",
                table: "tb_usuario",
                type: "text",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "text")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_usuario",
                keyColumn: "DS_SENHA",
                keyValue: null,
                column: "DS_SENHA",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DS_SENHA",
                table: "tb_usuario",
                type: "text",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }
    }
}
