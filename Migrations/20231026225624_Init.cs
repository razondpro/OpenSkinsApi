using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSkinsApi.Migrations
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
                values: new object[] { new Guid("ae72c8ce-fdbb-4eda-88a2-fa7f250c471b"), new DateTime(2023, 10, 26, 22, 56, 24, 113, DateTimeKind.Utc).AddTicks(7760), "johndoe@example.com", null });

            migrationBuilder.InsertData(
                table: "skins",
                columns: new[] { "id", "color", "created_on", "is_available", "last_modified_on", "name", "price", "type" },
                values: new object[,]
                {
                    { new Guid("101f3b98-cb0d-4929-a2ac-a5e8afd921b3"), "Pink", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6580), true, null, "Skin4", 29.99m, "Legendary" },
                    { new Guid("2794c8d0-9665-40fc-a24f-c9b534b38401"), "Brown", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6590), false, null, "Skin6", 12.99m, "Rare" },
                    { new Guid("3a36d8fb-5d1c-4e6d-9b51-4d59e029c181"), "Blue", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6590), true, null, "Skin5", 24.99m, "Normal" },
                    { new Guid("3ebcb0df-4c44-400c-a462-d72373afb22a"), "Yellow", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6560), false, null, "Skin1", 19.99m, "Rare" },
                    { new Guid("4c462570-c196-46f2-a1d7-e1fd4b522ed9"), "Purple", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6580), true, null, "Skin3", 15.99m, "Epic" },
                    { new Guid("5cf6e618-ea7d-4aa8-b051-c5b7d6c11191"), "Orange", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6600), true, null, "Skin8", 14.99m, "Legendary" },
                    { new Guid("65a9ce90-9430-4988-aa89-a4308d3291c5"), "Yellow", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6610), false, null, "Skin9", 22.99m, "Normal" },
                    { new Guid("80762e49-265b-4466-9648-a6f6f0b5f3cf"), "Red", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6600), true, null, "Skin7", 17.99m, "Epic" },
                    { new Guid("87af4649-9dd4-414b-990f-211083cfa162"), "Green", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6620), false, null, "Skin11", 24.99m, "Epic" },
                    { new Guid("bd6b1eef-4c5e-4672-852c-394e6849c518"), "White", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6610), true, null, "Skin10", 19.99m, "Rare" },
                    { new Guid("d87ad62e-ca2c-49e0-9168-a07b84f59593"), "Green", new DateTime(2023, 10, 26, 22, 56, 24, 114, DateTimeKind.Utc).AddTicks(6570), true, null, "Skin2", 9.99m, "Normal" }
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
