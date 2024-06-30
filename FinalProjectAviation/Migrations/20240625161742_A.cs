using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProjectAviation.Migrations
{
    /// <inheritdoc />
    public partial class A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LASTNAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    USERROLE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PASSENGERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PHONENUMBER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ETHNICITY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASSENGERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PASSENGERS_USERS",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PILOTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PHONENUMBER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ETHNICITY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PILOTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PILOTS_USERS",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FLIGHTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FROMCITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TOCITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TICKETPRICE = table.Column<int>(type: "int", nullable: false),
                    PilotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLIGHTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PILOTS_FLIGHTS",
                        column: x => x.PilotId,
                        principalTable: "PILOTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PASSENGERS_FLIGHTS",
                columns: table => new
                {
                    FlightsId = table.Column<int>(type: "int", nullable: false),
                    PassengersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PASSENGERS_FLIGHTS", x => new { x.FlightsId, x.PassengersId });
                    table.ForeignKey(
                        name: "FK_PASSENGERS_FLIGHTS_FLIGHTS_FlightsId",
                        column: x => x.FlightsId,
                        principalTable: "FLIGHTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PASSENGERS_FLIGHTS_PASSENGERS_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "PASSENGERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FLIGHTS_PilotId",
                table: "FLIGHTS",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_PASSENGERS_UserId",
                table: "PASSENGERS",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PASSENGERS_FLIGHTS_PassengersId",
                table: "PASSENGERS_FLIGHTS",
                column: "PassengersId");

            migrationBuilder.CreateIndex(
                name: "IX_PILOTS_UserId",
                table: "PILOTS",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LASTNAME",
                table: "USERS",
                column: "LASTNAME");

            migrationBuilder.CreateIndex(
                name: "UQ_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true,
                filter: "[EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_USERNAME",
                table: "USERS",
                column: "USERNAME",
                unique: true,
                filter: "[USERNAME] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PASSENGERS_FLIGHTS");

            migrationBuilder.DropTable(
                name: "FLIGHTS");

            migrationBuilder.DropTable(
                name: "PASSENGERS");

            migrationBuilder.DropTable(
                name: "PILOTS");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
