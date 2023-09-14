using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AATAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AvailableSeats", "Date", "Name", "TotalSeats" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2023, 9, 11, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5902), "Event 1", 100 },
                    { 2, 50, new DateTime(2023, 9, 18, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5911), "Event 2", 50 }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "Id", "Email", "EventId", "Name", "ReferenceNumber", "RegistrationDate", "UserId", "UserIdentifier" },
                values: new object[,]
                {
                    { 1, "user1@example.com", 1, "User One", "ABC123", new DateTime(2023, 9, 11, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5997), new Guid("d74e5dd7-a10e-418e-81fc-9265299f2424"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, "user2@example.com", 1, "User Two", "XYZ456", new DateTime(2023, 9, 11, 12, 39, 35, 902, DateTimeKind.Local).AddTicks(5998), new Guid("e5ceb6a0-69eb-4de2-a29a-413533968096"), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_EventId",
                table: "Registrations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_ReferenceNumber",
                table: "Registrations",
                column: "ReferenceNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
