using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyKPI.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dashboard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dashboard_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataWH",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true),
                    ConnectionType = table.Column<string>(nullable: true),
                    DataWHName = table.Column<string>(nullable: true),
                    DataWHServer = table.Column<string>(nullable: true),
                    DataWHUser = table.Column<string>(nullable: true),
                    DataWHPassword = table.Column<string>(nullable: true),
                    AuthWindows = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DateModification = table.Column<DateTime>(nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataWH", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataWH_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Intitule = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ReportId_BI = table.Column<string>(nullable: true),
                    WorkspaceId_BI = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DateModification = table.Column<DateTime>(nullable: false),
                    DateCreation = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataWHAccess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccess = table.Column<int>(nullable: false),
                    DataWHId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataWHAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataWHAccess_DataWH_DataWHId",
                        column: x => x.DataWHId,
                        principalTable: "DataWH",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataWHAccess_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportsAccess",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccess = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportsAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportsAccess_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportsAccess_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dashboard_UserId",
                table: "Dashboard",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DataWH_UserId",
                table: "DataWH",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DataWHAccess_DataWHId",
                table: "DataWHAccess",
                column: "DataWHId");

            migrationBuilder.CreateIndex(
                name: "IX_DataWHAccess_UserId",
                table: "DataWHAccess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportsAccess_ReportId",
                table: "ReportsAccess",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportsAccess_UserId",
                table: "ReportsAccess",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dashboard");

            migrationBuilder.DropTable(
                name: "DataWHAccess");

            migrationBuilder.DropTable(
                name: "ReportsAccess");

            migrationBuilder.DropTable(
                name: "DataWH");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
