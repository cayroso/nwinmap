﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NWINMAP.Web.Data;

namespace NWINMAP.Web.Migrations
{
    [DbContext(typeof(IdentityWebContext))]
    [Migration("20200604113701_app")]
    partial class app
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = "administrator",
                            ConcurrencyStamp = "6dd06960-176b-4b7c-9c8a-54c80f5f1f26",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "user",
                            ConcurrencyStamp = "060b6d29-70d3-4e3a-889a-0792a8e1a3b0",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserId = "systemadministrator1",
                            RoleId = "administrator"
                        },
                        new
                        {
                            UserId = "systemadministrator1",
                            RoleId = "user"
                        },
                        new
                        {
                            UserId = "systemadministrator2",
                            RoleId = "administrator"
                        },
                        new
                        {
                            UserId = "systemadministrator3",
                            RoleId = "administrator"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("NWINMAP.Web.Areas.Identity.Data.App.Barangay", b =>
                {
                    b.Property<string>("BarangayId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BarangayId");

                    b.ToTable("Barangay");

                    b.HasData(
                        new
                        {
                            BarangayId = "Barangay1",
                            Description = "muzon",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Muzon"
                        },
                        new
                        {
                            BarangayId = "Barangay2",
                            Description = "poblacion",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Poblacion"
                        },
                        new
                        {
                            BarangayId = "Barangay3",
                            Description = "calumpang east",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Calumpang East"
                        },
                        new
                        {
                            BarangayId = "Barangay4",
                            Description = "calumpang west",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Calumpang West"
                        },
                        new
                        {
                            BarangayId = "Barangay5",
                            Description = "taliba",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Taliba"
                        },
                        new
                        {
                            BarangayId = "Barangay6",
                            Description = "tunggal",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Tunggal"
                        },
                        new
                        {
                            BarangayId = "Barangay7",
                            Description = "tejero",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Tejero"
                        },
                        new
                        {
                            BarangayId = "Barangay8",
                            Description = "bagong tubig",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Bagong Tubig"
                        },
                        new
                        {
                            BarangayId = "Barangay9",
                            Description = "Banoyo",
                            Latitude = 13.842046,
                            Longitude = 120.943854,
                            Name = "Banoyo"
                        });
                });

            modelBuilder.Entity("NWINMAP.Web.Areas.Identity.Data.App.BarangayUserRole", b =>
                {
                    b.Property<string>("BarangayId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.HasKey("BarangayId", "UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("BarangayUserRole");

                    b.HasData(
                        new
                        {
                            BarangayId = "Barangay1",
                            UserId = "systemadministrator1",
                            RoleId = "user"
                        },
                        new
                        {
                            BarangayId = "Barangay2",
                            UserId = "systemadministrator1",
                            RoleId = "user"
                        });
                });

            modelBuilder.Entity("NWINMAP.Web.Areas.Identity.Data.App.Item", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("BarangayId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Image")
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemType")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("ItemId");

                    b.HasIndex("BarangayId");

                    b.HasIndex("UserId");

                    b.ToTable("Item");

                    b.HasData(
                        new
                        {
                            ItemId = "Barangay1Item1",
                            Address = "Address 1",
                            BarangayId = "Barangay1",
                            ConcurrencyStamp = "a4f96d34-95a1-4b40-bb4e-4f485b188f64",
                            Description = "Barangay Number 1 Item Number 1",
                            ItemStatus = -1,
                            ItemType = 1,
                            Latitude = 13.842146,
                            Longitude = 120.92385400000001,
                            Name = "Barangay #1 Item #1",
                            UserId = "systemadministrator1"
                        },
                        new
                        {
                            ItemId = "Barangay1Item2",
                            Address = "Address 2",
                            BarangayId = "Barangay1",
                            ConcurrencyStamp = "b5653ac9-8d5a-4915-8ed3-e39b8eb472ec",
                            Description = "Barangay Number 1 Item Number 2",
                            ItemStatus = 1,
                            ItemType = 2,
                            Latitude = 13.812346,
                            Longitude = 120.943854,
                            Name = "Barangay #1 Item #2",
                            UserId = "systemadministrator1"
                        },
                        new
                        {
                            ItemId = "Barangay1Item3",
                            Address = "Address 3",
                            BarangayId = "Barangay1",
                            ConcurrencyStamp = "aba7fa12-65e1-487a-a637-c53785902d03",
                            Description = "Barangay Number 1 Item Number 3",
                            ItemStatus = 0,
                            ItemType = 1,
                            Latitude = 13.862546,
                            Longitude = 120.943854,
                            Name = "Barangay #1 Item #3",
                            UserId = "systemadministrator2"
                        },
                        new
                        {
                            ItemId = "Barangay2Item4",
                            Address = "Address 4",
                            BarangayId = "Barangay2",
                            ConcurrencyStamp = "f2d7b231-aa85-4bbc-aac1-09ffc6bf40cb",
                            Description = "Barangay Number 2 Item Number 4",
                            ItemStatus = 1,
                            ItemType = 1,
                            Latitude = 13.862546,
                            Longitude = 120.963854,
                            Name = "Barangay #2 Item #4",
                            UserId = "systemadministrator2"
                        });
                });

            modelBuilder.Entity("NWINMAP.Web.Areas.Identity.Data.IdentityWebUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("UserId")
                        .HasColumnType("TEXT")
                        .HasMaxLength(36);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = "systemadministrator1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7af7285e-ba87-4f2b-b729-14e5b74bb8a1",
                            Email = "systemadministrator1@web.com",
                            EmailConfirmed = true,
                            FirstName = "Kristine",
                            LastName = "Pilac",
                            LockoutEnabled = false,
                            NormalizedEmail = "SYSTEMADMINISTRATOR1@WEB.COM",
                            NormalizedUserName = "SYSTEMADMINISTRATOR1@WEB.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
                            PhoneNumber = "09876543212",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
                            TwoFactorEnabled = false,
                            UserName = "systemadministrator1@web.com"
                        },
                        new
                        {
                            Id = "systemadministrator2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7af7285e-ba87-4f2b-b729-14e5b74bb8a1",
                            Email = "systemadministrator2@web.com",
                            EmailConfirmed = true,
                            FirstName = "Jovie Anne",
                            LastName = "Adajar",
                            LockoutEnabled = false,
                            NormalizedEmail = "SYSTEMADMINISTRATOR2@WEB.COM",
                            NormalizedUserName = "SYSTEMADMINISTRATOR2@WEB.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
                            PhoneNumber = "09876543212",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
                            TwoFactorEnabled = false,
                            UserName = "systemadministrator2@web.com"
                        },
                        new
                        {
                            Id = "systemadministrator3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7af7285e-ba87-4f2b-b729-14e5b74bb8a1",
                            Email = "systemadministrator3@web.com",
                            EmailConfirmed = true,
                            FirstName = "Nikko",
                            LastName = "Palamarez",
                            LockoutEnabled = false,
                            NormalizedEmail = "SYSTEMADMINISTRATOR3@WEB.COM",
                            NormalizedUserName = "SYSTEMADMINISTRATOR3@WEB.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
                            PhoneNumber = "09876543212",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
                            TwoFactorEnabled = false,
                            UserName = "systemadministrator3@web.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.IdentityWebUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.IdentityWebUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.IdentityWebUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.IdentityWebUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NWINMAP.Web.Areas.Identity.Data.App.BarangayUserRole", b =>
                {
                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.App.Barangay", "Barangay")
                        .WithMany("UserRoles")
                        .HasForeignKey("BarangayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.IdentityWebUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NWINMAP.Web.Areas.Identity.Data.App.Item", b =>
                {
                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.App.Barangay", "Barangay")
                        .WithMany("Items")
                        .HasForeignKey("BarangayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NWINMAP.Web.Areas.Identity.Data.IdentityWebUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
