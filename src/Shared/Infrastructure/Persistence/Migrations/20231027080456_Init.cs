using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSkinsApi.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "owners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owners", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "skins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    is_available = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skins", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "purchases",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    owner_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    skin_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    color = table.Column<int>(type: "int", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchases", x => new { x.id, x.owner_id, x.skin_id });
                    table.ForeignKey(
                        name: "FK_purchases_owners_owner_id",
                        column: x => x.owner_id,
                        principalTable: "owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchases_skins_skin_id",
                        column: x => x.skin_id,
                        principalTable: "skins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "owners",
                columns: new[] { "id", "created_on", "email", "last_modified_on" },
                values: new object[] { new Guid("65bfa21c-43f4-4430-80b2-97d82d40a0d5"), new DateTime(2023, 10, 27, 8, 4, 56, 440, DateTimeKind.Utc).AddTicks(9000), "johndoe@example.com", null });

            migrationBuilder.InsertData(
                table: "skins",
                columns: new[] { "id", "color", "created_on", "is_available", "last_modified_on", "name", "price", "type" },
                values: new object[,]
                {
                    { new Guid("0e238b35-a0ce-4760-ac73-a41f9b07ab59"), "Green", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7730), false, null, "Skin11", 24.99m, "Epic" },
                    { new Guid("241a1ca9-f26e-40f6-8ff7-5c039dc07874"), "Orange", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7720), true, null, "Skin8", 14.99m, "Legendary" },
                    { new Guid("49e7fdf6-90bf-41fb-9bb2-3d8cfb0f9f5e"), "Red", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7720), true, null, "Skin7", 17.99m, "Epic" },
                    { new Guid("5994e1d8-d643-4eb1-8f51-8bf1caa3edca"), "Purple", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7700), true, null, "Skin3", 15.99m, "Epic" },
                    { new Guid("6f8d1cf3-8538-4ae9-9264-a9f5af66fe88"), "Blue", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7710), true, null, "Skin5", 24.99m, "Normal" },
                    { new Guid("93d79334-fcae-4344-9b61-68a5f60bc737"), "Brown", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7710), false, null, "Skin6", 12.99m, "Rare" },
                    { new Guid("ca99ed0c-9c71-47c5-8d28-1e26456553dd"), "Pink", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7700), true, null, "Skin4", 29.99m, "Legendary" },
                    { new Guid("d3cc6d9f-1bbb-4b22-b1ed-93bbb4b74f64"), "Green", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7690), true, null, "Skin2", 9.99m, "Normal" },
                    { new Guid("d7e90c6c-c9f4-481c-8f1e-b634b1069594"), "Yellow", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7720), false, null, "Skin9", 22.99m, "Normal" },
                    { new Guid("e477e5f3-ddfa-4213-92b8-21a191632810"), "White", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7730), true, null, "Skin10", 19.99m, "Rare" },
                    { new Guid("fe295ef3-7e62-4c0a-aa93-70610a940162"), "Yellow", new DateTime(2023, 10, 27, 8, 4, 56, 441, DateTimeKind.Utc).AddTicks(7690), false, null, "Skin1", 19.99m, "Rare" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_owners_email",
                table: "owners",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchases_owner_id",
                table: "purchases",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchases_skin_id",
                table: "purchases",
                column: "skin_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.DropTable(
                name: "owners");

            migrationBuilder.DropTable(
                name: "skins");
        }
    }
}
