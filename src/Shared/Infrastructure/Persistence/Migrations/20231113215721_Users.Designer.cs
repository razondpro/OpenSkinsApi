﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenSkinsApi.Infrastructure.Persistence;

#nullable disable

namespace OpenSkinsApi.src.Shared.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20231113215721_Users")]
    partial class Users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OpenSkinsApi.Infrastructure.Persistence.Core.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("data");

                    b.Property<string>("Error")
                        .HasColumnType("longtext")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("occurred_on");

                    b.Property<DateTime?>("ProcessedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("processed_on");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("OpenSkinsApi.Infrastructure.Persistence.Core.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<Guid>("EventId")
                        .HasColumnType("char(36)")
                        .HasColumnName("event_id");

                    b.Property<string>("EventType")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("event_type");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("EventId", "EventType");

                    b.ToTable("outbox_messages_consumer", (string)null);
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Auth.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<DateTime>("ExpiresOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("expires_on");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_revoked");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_used");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("token");

                    b.Property<Guid>("user_id")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.HasIndex("user_id");

                    b.ToTable("auth_refresh_tokens", (string)null);
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Auth.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("user_name");

                    b.Property<bool>("Verified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("verified");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("auth_users", (string)null);
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Skins.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_modified_on");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("skins_owners", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9e8cac5-4f94-463a-9bfc-631c04e1375e"),
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 531, DateTimeKind.Utc).AddTicks(5970),
                            Email = "johndoe@example.com"
                        });
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Skins.Domain.Entities.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("char(36)")
                        .HasColumnName("owner_id");

                    b.Property<Guid>("SkinId")
                        .HasColumnType("char(36)")
                        .HasColumnName("skin_id");

                    b.Property<int>("Color")
                        .HasColumnType("int")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("deleted_at");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_modified_on");

                    b.HasKey("Id", "OwnerId", "SkinId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SkinId");

                    b.ToTable("skins_purchases", (string)null);
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Skins.Domain.Entities.Skin", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_available");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("skins_skins", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("43b19e25-37e5-49a7-8f83-26abc5441770"),
                            Color = "Yellow",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1850),
                            IsAvailable = false,
                            Name = "Skin1",
                            Price = 19.99m,
                            Type = "Rare"
                        },
                        new
                        {
                            Id = new Guid("9856734c-16e9-433a-a6f7-f4e8cfaa93ee"),
                            Color = "Green",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1860),
                            IsAvailable = true,
                            Name = "Skin2",
                            Price = 9.99m,
                            Type = "Normal"
                        },
                        new
                        {
                            Id = new Guid("21fdbb74-3607-4402-b4de-13f5cae2055f"),
                            Color = "Purple",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1860),
                            IsAvailable = true,
                            Name = "Skin3",
                            Price = 15.99m,
                            Type = "Epic"
                        },
                        new
                        {
                            Id = new Guid("ba866817-d6ca-4436-8dab-7c33dee96849"),
                            Color = "Pink",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1870),
                            IsAvailable = true,
                            Name = "Skin4",
                            Price = 29.99m,
                            Type = "Legendary"
                        },
                        new
                        {
                            Id = new Guid("cb3ae8ab-4945-4205-802a-5117b40e316d"),
                            Color = "Blue",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1870),
                            IsAvailable = true,
                            Name = "Skin5",
                            Price = 24.99m,
                            Type = "Normal"
                        },
                        new
                        {
                            Id = new Guid("bc12e490-a4c6-44f2-8f05-83dae3273b54"),
                            Color = "Brown",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1870),
                            IsAvailable = false,
                            Name = "Skin6",
                            Price = 12.99m,
                            Type = "Rare"
                        },
                        new
                        {
                            Id = new Guid("ae1599eb-ab94-483c-9489-daa59cfce12e"),
                            Color = "Red",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1880),
                            IsAvailable = true,
                            Name = "Skin7",
                            Price = 17.99m,
                            Type = "Epic"
                        },
                        new
                        {
                            Id = new Guid("7c7ce195-2513-41d6-9f30-3e2c5573cca2"),
                            Color = "Orange",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1890),
                            IsAvailable = true,
                            Name = "Skin8",
                            Price = 14.99m,
                            Type = "Legendary"
                        },
                        new
                        {
                            Id = new Guid("417d813f-23a2-44b9-aef8-f93410171d1d"),
                            Color = "Yellow",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1890),
                            IsAvailable = false,
                            Name = "Skin9",
                            Price = 22.99m,
                            Type = "Normal"
                        },
                        new
                        {
                            Id = new Guid("682d1f1c-4a3b-4e4b-a219-b7a1d5de3cfa"),
                            Color = "White",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1890),
                            IsAvailable = true,
                            Name = "Skin10",
                            Price = 19.99m,
                            Type = "Rare"
                        },
                        new
                        {
                            Id = new Guid("c771f725-7c93-47bd-a885-d4f999fa18b0"),
                            Color = "Green",
                            CreatedOn = new DateTime(2023, 11, 13, 21, 57, 21, 532, DateTimeKind.Utc).AddTicks(1900),
                            IsAvailable = false,
                            Name = "Skin11",
                            Price = 24.99m,
                            Type = "Epic"
                        });
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Users.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_on");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_modified_on");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("users_users", (string)null);
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Auth.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("OpenSkinsApi.Modules.Auth.Domain.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Auth.Domain.Entities.User", b =>
                {
                    b.OwnsOne("OpenSkinsApi.Modules.Auth.Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<byte[]>("Hash")
                                .IsRequired()
                                .HasColumnType("blob")
                                .HasColumnName("password_hash");

                            b1.Property<byte[]>("Salt")
                                .IsRequired()
                                .HasColumnType("blob")
                                .HasColumnName("password_salt");

                            b1.HasKey("UserId");

                            b1.ToTable("auth_users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Password");
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Skins.Domain.Entities.Purchase", b =>
                {
                    b.HasOne("OpenSkinsApi.Modules.Skins.Domain.Entities.Owner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenSkinsApi.Modules.Skins.Domain.Entities.Skin", "Skin")
                        .WithMany("Purchases")
                        .HasForeignKey("SkinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Skin");
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Users.Domain.Entities.User", b =>
                {
                    b.OwnsOne("OpenSkinsApi.Modules.Users.Domain.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)")
                                .HasColumnName("last_name");

                            b1.HasKey("UserId");

                            b1.ToTable("users_users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Auth.Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("OpenSkinsApi.Modules.Skins.Domain.Entities.Skin", b =>
                {
                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}