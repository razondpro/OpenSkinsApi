using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OpenSkinsApi.src.Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "auth_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<byte[]>(type: "blob", nullable: true),
                    password_salt = table.Column<byte[]>(type: "blob", nullable: true),
                    verified = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "skins_owners",
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
                    table.PrimaryKey("PK_skins_owners", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "skins_skins",
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
                    table.PrimaryKey("PK_skins_skins", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "auth_refresh_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    token = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_used = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_revoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    expires_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auth_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_auth_refresh_tokens_auth_users_user_id",
                        column: x => x.user_id,
                        principalTable: "auth_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "skins_purchases",
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
                    table.PrimaryKey("PK_skins_purchases", x => new { x.id, x.owner_id, x.skin_id });
                    table.ForeignKey(
                        name: "FK_skins_purchases_skins_owners_owner_id",
                        column: x => x.owner_id,
                        principalTable: "skins_owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_skins_purchases_skins_skins_skin_id",
                        column: x => x.skin_id,
                        principalTable: "skins_skins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "skins_owners",
                columns: new[] { "id", "created_on", "email", "last_modified_on" },
                values: new object[] { new Guid("c9e8cac5-4f94-463a-9bfc-631c04e1375e"), new DateTime(2023, 11, 13, 21, 57, 21, 531, DateTimeKind.Utc).AddTicks(5970), "johndoe@example.com", null });

            migrationBuilder.InsertData(
                table: "skins_skins",
                columns: new[] { "id", "color", "created_on", "is_available", "last_modified_on", "name", "price", "type" },
                values: new object[,]
                {
                    { new Guid("21fdbb74-3607-4402-b4de-13f5cae2055f"), "Purple", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1860), true, null, "Skin3", 15.99m, "Epic" },
                    { new Guid("417d813f-23a2-44b9-aef8-f93410171d1d"), "Yellow", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1890), false, null, "Skin9", 22.99m, "Normal" },
                    { new Guid("43b19e25-37e5-49a7-8f83-26abc5441770"), "Yellow", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1850), false, null, "Skin1", 19.99m, "Rare" },
                    { new Guid("682d1f1c-4a3b-4e4b-a219-b7a1d5de3cfa"), "White", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1890), true, null, "Skin10", 19.99m, "Rare" },
                    { new Guid("7c7ce195-2513-41d6-9f30-3e2c5573cca2"), "Orange", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1890), true, null, "Skin8", 14.99m, "Legendary" },
                    { new Guid("9856734c-16e9-433a-a6f7-f4e8cfaa93ee"), "Green", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1860), true, null, "Skin2", 9.99m, "Normal" },
                    { new Guid("ae1599eb-ab94-483c-9489-daa59cfce12e"), "Red", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1880), true, null, "Skin7", 17.99m, "Epic" },
                    { new Guid("ba866817-d6ca-4436-8dab-7c33dee96849"), "Pink", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1870), true, null, "Skin4", 29.99m, "Legendary" },
                    { new Guid("bc12e490-a4c6-44f2-8f05-83dae3273b54"), "Brown", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1870), false, null, "Skin6", 12.99m, "Rare" },
                    { new Guid("c771f725-7c93-47bd-a885-d4f999fa18b0"), "Green", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1900), false, null, "Skin11", 24.99m, "Epic" },
                    { new Guid("cb3ae8ab-4945-4205-802a-5117b40e316d"), "Blue", new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1870), true, null, "Skin5", 24.99m, "Normal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_auth_refresh_tokens_token",
                table: "auth_refresh_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_refresh_tokens_user_id",
                table: "auth_refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_auth_users_email",
                table: "auth_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auth_users_user_name",
                table: "auth_users",
                column: "user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_outbox_messages_type",
                table: "outbox_messages",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_outbox_messages_consumer_event_id_event_type",
                table: "outbox_messages_consumer",
                columns: new[] { "event_id", "event_type" });

            migrationBuilder.CreateIndex(
                name: "IX_skins_owners_email",
                table: "skins_owners",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_skins_purchases_owner_id",
                table: "skins_purchases",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_skins_purchases_skin_id",
                table: "skins_purchases",
                column: "skin_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_users_user_name",
                table: "users_users",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_refresh_tokens");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "outbox_messages_consumer");

            migrationBuilder.DropTable(
                name: "skins_purchases");

            migrationBuilder.DropTable(
                name: "users_users");

            migrationBuilder.DropTable(
                name: "auth_users");

            migrationBuilder.DropTable(
                name: "skins_owners");

            migrationBuilder.DropTable(
                name: "skins_skins");
        }
    }
}
