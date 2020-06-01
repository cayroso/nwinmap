using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NWINMAP.Web.Migrations
{
    public partial class identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Barangay",
                columns: table => new
                {
                    BarangayId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barangay", x => x.BarangayId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarangayUserRole",
                columns: table => new
                {
                    BarangayId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangayUserRole", x => new { x.BarangayId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_BarangayUserRole_Barangay_BarangayId",
                        column: x => x.BarangayId,
                        principalTable: "Barangay",
                        principalColumn: "BarangayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarangayUserRole_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarangayUserRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<string>(nullable: false),
                    BarangayId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ItemType = table.Column<int>(nullable: false),
                    ItemStatus = table.Column<int>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Barangay_BarangayId",
                        column: x => x.BarangayId,
                        principalTable: "Barangay",
                        principalColumn: "BarangayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "systemadministrator", "e30fe687-debd-4159-aa76-da3e6013e065", "SystemAdministrator", "SYSTEMADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "administrator", "f921b063-e23c-4c5e-a21f-e6882b7314b2", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "systemadministrator1", 0, "7af7285e-ba87-4f2b-b729-14e5b74bb8a1", "systemadministrator1@web.com", true, "SysAdmin1 FirstName", "SysAdmin1 LastName", false, null, "SYSTEMADMINISTRATOR1@WEB.COM", "SYSTEMADMINISTRATOR1@WEB.COM", "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==", "09876543212", true, "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM", false, "systemadministrator1@web.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "systemadministrator2", 0, "7af7285e-ba87-4f2b-b729-14e5b74bb8a1", "systemadministrator2@web.com", true, "SysAdmin2 FirstName", "SysAdmin2 LastName", false, null, "SYSTEMADMINISTRATOR2@WEB.COM", "SYSTEMADMINISTRATOR2@WEB.COM", "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==", "09876543212", true, "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM", false, "systemadministrator2@web.com" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay1", null, "Barangay Number 1", 13.842046, 120.943854, "Barangay #1" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay2", null, "Barangay Number 2", 13.842046, 120.943854, "Barangay #2" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay3", null, "Barangay Number 3", 13.842046, 120.943854, "Barangay #3" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay4", null, "Barangay Number 4", 13.842046, 120.943854, "Barangay #4" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay5", null, "Barangay Number 5", 13.842046, 120.943854, "Barangay #5" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "systemadministrator1", "systemadministrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "systemadministrator2", "systemadministrator" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay1Item1", "Address 1", "Barangay1", "222721fb-8202-4aff-8875-ab7ec4215d44", "Barangay Number 1 Item Number 1", null, -1, 1, 13.842146, 120.92385400000001, "Barangay #1 Item #1", null, "systemadministrator1" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay1Item2", "Address 2", "Barangay1", "c24c79ac-c9fd-40b8-8511-22322f48ac88", "Barangay Number 1 Item Number 2", null, 1, 2, 13.812346, 120.943854, "Barangay #1 Item #2", null, "systemadministrator1" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay1Item3", "Address 3", "Barangay1", "3f44c226-a938-4fed-ab10-6f7e273376c1", "Barangay Number 1 Item Number 3", null, 0, 1, 13.862546, 120.943854, "Barangay #1 Item #3", null, "systemadministrator2" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay2Item4", "Address 4", "Barangay2", "b293a45a-4698-4771-b2c9-7edfdf9d9bd1", "Barangay Number 2 Item Number 4", null, 1, 1, 13.862546, 120.963854, "Barangay #2 Item #4", null, "systemadministrator2" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarangayUserRole_RoleId",
                table: "BarangayUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_BarangayUserRole_UserId",
                table: "BarangayUserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BarangayId",
                table: "Item",
                column: "BarangayId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UserId",
                table: "Item",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BarangayUserRole");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Barangay");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
