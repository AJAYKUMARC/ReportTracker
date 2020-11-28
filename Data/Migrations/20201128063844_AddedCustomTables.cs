using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportTracker.Data.Migrations
{
    public partial class AddedCustomTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileTrackerInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(maxLength: 250, nullable: false),
                    BarCode = table.Column<string>(type: "text", nullable: false),
                    Dept_From = table.Column<int>(nullable: false),
                    Dept_To = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTrackerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileTracker_Department_From",
                        column: x => x.Dept_From,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileTracker_Department_To",
                        column: x => x.Dept_To,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileTrackerInfo_Dept_From",
                table: "FileTrackerInfo",
                column: "Dept_From");

            migrationBuilder.CreateIndex(
                name: "IX_FileTrackerInfo_Dept_To",
                table: "FileTrackerInfo",
                column: "Dept_To");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileTrackerInfo");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
