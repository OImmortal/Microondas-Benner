using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroondasMVC_Benner.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "UserAuth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "UserAuth",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
