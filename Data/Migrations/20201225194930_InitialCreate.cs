using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantCharacteristics",
                columns: table => new
                {
                    Guid = table.Column<byte[]>(nullable: false),
                    Notes = table.Column<string>(maxLength: 8000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCharacteristics", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Guid = table.Column<byte[]>(nullable: false),
                    Nickname = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 254, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Guid = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PlantCharacteristicId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Plants_PlantCharacteristics_PlantCharacteristicId",
                        column: x => x.PlantCharacteristicId,
                        principalTable: "PlantCharacteristics",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoogleAuths",
                columns: table => new
                {
                    Guid = table.Column<byte[]>(nullable: false),
                    Sub = table.Column<string>(maxLength: 512, nullable: true),
                    UserId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleAuths", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_GoogleAuths_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantCollections",
                columns: table => new
                {
                    Guid = table.Column<byte[]>(nullable: false),
                    Nickname = table.Column<string>(maxLength: 100, nullable: true),
                    Location = table.Column<string>(maxLength: 100, nullable: true),
                    Notes = table.Column<string>(maxLength: 8000, nullable: true),
                    UserId = table.Column<byte[]>(nullable: false),
                    PlantId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCollections", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_PlantCollections_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantCollections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WateringSchedules",
                columns: table => new
                {
                    Guid = table.Column<byte[]>(nullable: false),
                    DayOfWeek = table.Column<string>(maxLength: 25, nullable: false),
                    TimeOfDay = table.Column<DateTime>(nullable: false),
                    Repeat = table.Column<string>(maxLength: 25, nullable: false),
                    PlantCollectionId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WateringSchedules", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_WateringSchedules_PlantCollections_PlantCollectionId",
                        column: x => x.PlantCollectionId,
                        principalTable: "PlantCollections",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoogleAuths_UserId",
                table: "GoogleAuths",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlantCollections_PlantId",
                table: "PlantCollections",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCollections_UserId",
                table: "PlantCollections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_PlantCharacteristicId",
                table: "Plants",
                column: "PlantCharacteristicId");

            migrationBuilder.CreateIndex(
                name: "IX_WateringSchedules_PlantCollectionId",
                table: "WateringSchedules",
                column: "PlantCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoogleAuths");

            migrationBuilder.DropTable(
                name: "WateringSchedules");

            migrationBuilder.DropTable(
                name: "PlantCollections");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PlantCharacteristics");
        }
    }
}
