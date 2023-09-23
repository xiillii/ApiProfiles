using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPersonProfiles.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThumbnailFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Age", "DateCreated", "DateUpdated", "FirstName", "LastName", "Nickname" },
                values: new object[] { 1, 24, new DateTimeOffset(new DateTime(2023, 9, 23, 1, 4, 57, 743, DateTimeKind.Unspecified).AddTicks(7695), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 23, 1, 4, 57, 743, DateTimeKind.Unspecified).AddTicks(7697), new TimeSpan(0, -6, 0, 0, 0)), "James", "Bond", "007" });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "FileName", "ProfileId", "ThumbnailFileName" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2023, 9, 23, 1, 4, 57, 743, DateTimeKind.Unspecified).AddTicks(7472), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 9, 23, 1, 4, 57, 743, DateTimeKind.Unspecified).AddTicks(7496), new TimeSpan(0, -6, 0, 0, 0)), "Picture.jpg", 1, "Picture.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProfileId",
                table: "Pictures",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
