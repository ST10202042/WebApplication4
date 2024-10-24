using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    LectureID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfClaim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LecturerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupportingDocuments = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.LectureID);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    ModuleCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RatePerHour = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    DateOfClaim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimLectureID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ModuleCode);
                    table.ForeignKey(
                        name: "FK_Module_Claims_ClaimLectureID",
                        column: x => x.ClaimLectureID,
                        principalTable: "Claims",
                        principalColumn: "LectureID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Module_ClaimLectureID",
                table: "Module",
                column: "ClaimLectureID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Claims");
        }
    }
}
