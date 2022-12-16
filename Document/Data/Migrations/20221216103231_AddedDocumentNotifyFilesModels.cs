using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Document.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDocumentNotifyFilesModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentModels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentModels_CategoryModel_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "CategoryModel",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DocumentModels_CompanyModel_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "CompanyModel",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DocumentModels_ContactsModel_ContactID",
                        column: x => x.ContactID,
                        principalTable: "ContactsModel",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DocumentModels_LocationModel_LocationID",
                        column: x => x.LocationID,
                        principalTable: "LocationModel",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Files_DocumentModels_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "DocumentModels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "NotifyModels",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyModels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotifyModels_ContactsModel_ContactID",
                        column: x => x.ContactID,
                        principalTable: "ContactsModel",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NotifyModels_DocumentModels_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "DocumentModels",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentModels_CategoryID",
                table: "DocumentModels",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentModels_CompanyID",
                table: "DocumentModels",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentModels_ContactID",
                table: "DocumentModels",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentModels_LocationID",
                table: "DocumentModels",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_DocumentID",
                table: "Files",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyModels_ContactID",
                table: "NotifyModels",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyModels_DocumentID",
                table: "NotifyModels",
                column: "DocumentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "NotifyModels");

            migrationBuilder.DropTable(
                name: "DocumentModels");
        }
    }
}
