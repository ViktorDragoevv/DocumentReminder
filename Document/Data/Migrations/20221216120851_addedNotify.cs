using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Document.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedNotify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "NotifyModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Send",
                table: "NotifyModels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "NotifyModels");

            migrationBuilder.DropColumn(
                name: "Send",
                table: "NotifyModels");
        }
    }
}
