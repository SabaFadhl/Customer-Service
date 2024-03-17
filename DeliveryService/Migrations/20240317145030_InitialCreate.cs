using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "delivery");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "DriverDelivery",
                schema: "delivery",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    DeliverStatus = table.Column<string>(type: "text", nullable: false, defaultValue: "PickedUp"),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    OnwayTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveredTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverDelivery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryRequest",
                schema: "delivery",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValue: "Pending"),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DriverDeliveryId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryRequest_DriverDelivery_DriverDeliveryId",
                        column: x => x.DriverDeliveryId,
                        principalSchema: "delivery",
                        principalTable: "DriverDelivery",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                schema: "delivery",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    AvailabilityStatus = table.Column<string>(type: "text", nullable: false, defaultValue: "Online"),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DriverDeliveryId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Driver_DriverDelivery_DriverDeliveryId",
                        column: x => x.DriverDeliveryId,
                        principalSchema: "delivery",
                        principalTable: "DriverDelivery",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRequest_DriverDeliveryId",
                schema: "delivery",
                table: "DeliveryRequest",
                column: "DriverDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_DriverDeliveryId",
                schema: "delivery",
                table: "Driver",
                column: "DriverDeliveryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryRequest",
                schema: "delivery");

            migrationBuilder.DropTable(
                name: "Driver",
                schema: "delivery");

            migrationBuilder.DropTable(
                name: "DriverDelivery",
                schema: "delivery");
        }
    }
}
