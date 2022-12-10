using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Document.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLocationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "ContactsModel",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ContactsModel",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationID",
                table: "ContactsModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocationModel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationModel", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactsModel_LocationID",
                table: "ContactsModel",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactsModel_LocationModel_LocationID",
                table: "ContactsModel",
                column: "LocationID",
                principalTable: "LocationModel",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactsModel_LocationModel_LocationID",
                table: "ContactsModel");

            migrationBuilder.DropTable(
                name: "LocationModel");

            migrationBuilder.DropIndex(
                name: "IX_ContactsModel_LocationID",
                table: "ContactsModel");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "ContactsModel");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "ContactsModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ContactsModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
