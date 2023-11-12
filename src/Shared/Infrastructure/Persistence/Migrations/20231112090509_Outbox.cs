using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSkinsApi.src.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Outbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "owners",
                keyColumn: "id",
                keyValue: new Guid("65bfa21c-43f4-4430-80b2-97d82d40a0d5"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("0e238b35-a0ce-4760-ac73-a41f9b07ab59"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("241a1ca9-f26e-40f6-8ff7-5c039dc07874"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("49e7fdf6-90bf-41fb-9bb2-3d8cfb0f9f5e"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("5994e1d8-d643-4eb1-8f51-8bf1caa3edca"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("6f8d1cf3-8538-4ae9-9264-a9f5af66fe88"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("93d79334-fcae-4344-9b61-68a5f60bc737"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("ca99ed0c-9c71-47c5-8d28-1e26456553dd"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("d3cc6d9f-1bbb-4b22-b1ed-93bbb4b74f64"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("d7e90c6c-c9f4-481c-8f1e-b634b1069594"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("e477e5f3-ddfa-4213-92b8-21a191632810"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("fe295ef3-7e62-4c0a-aa93-70610a940162"));

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    occurred_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    processed_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    error = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox_messages", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "outbox_messages_consumer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    event_type = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox_messages_consumer", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "owners",
                columns: new[] { "id", "created_on", "email", "last_modified_on" },
                values: new object[] { new Guid("1bbe4b61-0615-40a2-8ca4-25ecb4522523"), new DateTime(2023, 11, 12, 9, 5, 9, 826, DateTimeKind.Utc).AddTicks(6280), "johndoe@example.com", null });

            migrationBuilder.InsertData(
                table: "skins",
                columns: new[] { "id", "color", "created_on", "is_available", "last_modified_on", "name", "price", "type" },
                values: new object[,]
                {
                    { new Guid("2e75a432-bcd4-4c31-90e9-732311de896a"), "Brown", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5150), false, null, "Skin6", 12.99m, "Rare" },
                    { new Guid("3b977d8a-a8df-4133-958c-4b82d968b9b8"), "White", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5160), true, null, "Skin10", 19.99m, "Rare" },
                    { new Guid("4ecb4880-9310-47f6-9ca3-2c7870e66920"), "Blue", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5140), true, null, "Skin5", 24.99m, "Normal" },
                    { new Guid("7645ec51-bafe-474f-99c6-8f62cb7fa323"), "Pink", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5140), true, null, "Skin4", 29.99m, "Legendary" },
                    { new Guid("7cec5ae0-4124-4c55-9e55-4c2035776ddb"), "Red", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5150), true, null, "Skin7", 17.99m, "Epic" },
                    { new Guid("8b463593-dd0e-4b2c-a93b-8c6ec7243c85"), "Green", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5170), false, null, "Skin11", 24.99m, "Epic" },
                    { new Guid("abd88146-1d3d-464f-9e6d-661be69d808e"), "Green", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5130), true, null, "Skin2", 9.99m, "Normal" },
                    { new Guid("b71e24f8-c5e2-4e83-9375-22bcf5949809"), "Yellow", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5120), false, null, "Skin1", 19.99m, "Rare" },
                    { new Guid("d95118bf-b443-49b7-9cc6-923d458903a9"), "Yellow", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5160), false, null, "Skin9", 22.99m, "Normal" },
                    { new Guid("da44cc7a-ac85-46e7-b373-4c626d4cd05a"), "Orange", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5150), true, null, "Skin8", 14.99m, "Legendary" },
                    { new Guid("f7231d85-6c48-4ba3-9870-40e57dcc8447"), "Purple", new DateTime(2023, 11, 12, 9, 5, 9, 827, DateTimeKind.Utc).AddTicks(5130), true, null, "Skin3", 15.99m, "Epic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_outbox_messages_type",
                table: "outbox_messages",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_outbox_messages_consumer_event_id_event_type",
                table: "outbox_messages_consumer",
                columns: new[] { "event_id", "event_type" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "outbox_messages_consumer");

            migrationBuilder.DeleteData(
                table: "owners",
                keyColumn: "id",
                keyValue: new Guid("1bbe4b61-0615-40a2-8ca4-25ecb4522523"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("2e75a432-bcd4-4c31-90e9-732311de896a"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("3b977d8a-a8df-4133-958c-4b82d968b9b8"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("4ecb4880-9310-47f6-9ca3-2c7870e66920"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("7645ec51-bafe-474f-99c6-8f62cb7fa323"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("7cec5ae0-4124-4c55-9e55-4c2035776ddb"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("8b463593-dd0e-4b2c-a93b-8c6ec7243c85"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("abd88146-1d3d-464f-9e6d-661be69d808e"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("b71e24f8-c5e2-4e83-9375-22bcf5949809"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("d95118bf-b443-49b7-9cc6-923d458903a9"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("da44cc7a-ac85-46e7-b373-4c626d4cd05a"));

            migrationBuilder.DeleteData(
                table: "skins",
                keyColumn: "id",
                keyValue: new Guid("f7231d85-6c48-4ba3-9870-40e57dcc8447"));

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
        }
    }
}
