using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NWINMAP.Web.Migrations
{
    public partial class app : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 36, nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
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
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
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
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarangayUserRole",
                columns: table => new
                {
                    BarangayId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarangayUserRole", x => new { x.BarangayId, x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_BarangayUserRole_Barangay_BarangayId",
                        column: x => x.BarangayId,
                        principalTable: "Barangay",
                        principalColumn: "BarangayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarangayUserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarangayUserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_Item_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
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
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay1", null, "muzon", 13.842046, 120.943854, "Muzon" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay2", null, "poblacion", 13.842046, 120.943854, "Poblacion" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay3", null, "calumpang east", 13.842046, 120.943854, "Calumpang East" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay4", null, "calumpang west", 13.842046, 120.943854, "Calumpang West" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay5", null, "taliba", 13.842046, 120.943854, "Taliba" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay6", null, "tunggal", 13.842046, 120.943854, "Tunggal" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay7", null, "tejero", 13.842046, 120.943854, "Tejero" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay8", null, "bagong tubig", 13.842046, 120.943854, "Bagong Tubig" });

            migrationBuilder.InsertData(
                table: "Barangay",
                columns: new[] { "BarangayId", "ConcurrencyStamp", "Description", "Latitude", "Longitude", "Name" },
                values: new object[] { "Barangay9", null, "Banoyo", 13.842046, 120.943854, "Banoyo" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "administrator", "6dd06960-176b-4b7c-9c8a-54c80f5f1f26", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "user", "060b6d29-70d3-4e3a-889a-0792a8e1a3b0", "User", "USER" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "systemadministrator1", 0, "7af7285e-ba87-4f2b-b729-14e5b74bb8a1", "systemadministrator1@web.com", true, "Kristine", "Pilac", false, null, "SYSTEMADMINISTRATOR1@WEB.COM", "SYSTEMADMINISTRATOR1@WEB.COM", "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==", "09876543212", true, "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM", false, "systemadministrator1@web.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "systemadministrator2", 0, "7af7285e-ba87-4f2b-b729-14e5b74bb8a1", "systemadministrator2@web.com", true, "Jovie Anne", "Adajar", false, null, "SYSTEMADMINISTRATOR2@WEB.COM", "SYSTEMADMINISTRATOR2@WEB.COM", "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==", "09876543212", true, "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM", false, "systemadministrator2@web.com" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "systemadministrator3", 0, "7af7285e-ba87-4f2b-b729-14e5b74bb8a1", "systemadministrator3@web.com", true, "Nikko", "Palamarez", false, null, "SYSTEMADMINISTRATOR3@WEB.COM", "SYSTEMADMINISTRATOR3@WEB.COM", "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==", "09876543212", true, "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM", false, "systemadministrator3@web.com" });

            migrationBuilder.InsertData(
                table: "BarangayUserRole",
                columns: new[] { "BarangayId", "UserId", "RoleId", "ConcurrencyStamp" },
                values: new object[] { "Barangay1", "systemadministrator1", "user", null });

            migrationBuilder.InsertData(
                table: "BarangayUserRole",
                columns: new[] { "BarangayId", "UserId", "RoleId", "ConcurrencyStamp" },
                values: new object[] { "Barangay2", "systemadministrator1", "user", null });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay1Item1", "Address 1", "Barangay1", "a4f96d34-95a1-4b40-bb4e-4f485b188f64", "Barangay Number 1 Item Number 1", null, -1, 1, 13.842146, 120.92385400000001, "Barangay #1 Item #1", null, "systemadministrator1" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay1Item2", "Address 2", "Barangay1", "b5653ac9-8d5a-4915-8ed3-e39b8eb472ec", "Barangay Number 1 Item Number 2", null, 1, 2, 13.812346, 120.943854, "Barangay #1 Item #2", null, "systemadministrator1" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay1Item3", "Address 3", "Barangay1", "aba7fa12-65e1-487a-a637-c53785902d03", "Barangay Number 1 Item Number 3", null, 0, 1, 13.862546, 120.943854, "Barangay #1 Item #3", null, "systemadministrator2" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Address", "BarangayId", "ConcurrencyStamp", "Description", "Image", "ItemStatus", "ItemType", "Latitude", "Longitude", "Name", "Source", "UserId" },
                values: new object[] { "Barangay2Item4", "Address 4", "Barangay2", "f2d7b231-aa85-4bbc-aac1-09ffc6bf40cb", "Barangay Number 2 Item Number 4", null, 1, 1, 13.862546, 120.963854, "Barangay #2 Item #4", null, "systemadministrator2" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "systemadministrator1", "administrator" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "systemadministrator1", "user" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "systemadministrator2", "administrator" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "systemadministrator3", "administrator" });

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

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarangayUserRole");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Barangay");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
