using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NWINMAP.Web.Areas.Identity.Constants;
using NWINMAP.Web.Areas.Identity.Data;
using NWINMAP.Web.Areas.Identity.Data.App;

namespace NWINMAP.Web.Data
{
    public class IdentityWebContext : IdentityDbContext<IdentityWebUser>
    {
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<Item> Items { get; set; }

        public IdentityWebContext(DbContextOptions<IdentityWebContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            builder.Entity<Barangay>(b =>
            {
                b.ToTable("Barangay");

                b.HasKey(e => e.BarangayId);

                b.Property(e => e.Name).IsRequired();
                b.Property(e => e.Latitude).IsRequired();
                b.Property(e => e.Longitude).IsRequired();

                b.HasMany(e => e.Items)
                    .WithOne(d => d.Barangay)
                    .HasForeignKey(d => d.BarangayId)
                    .IsRequired();

                b.HasMany(e => e.UserRoles)
                    .WithOne(d => d.Barangay)
                    .HasForeignKey(e => e.BarangayId)
                    .IsRequired();

                b.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
            });

            builder.Entity<BarangayUserRole>(b =>
            {
                b.ToTable("BarangayUserRole");

                b.HasKey(e => new { e.BarangayId, e.RoleId });
                b.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
            });

            builder.Entity<Item>(b =>
            {
                b.ToTable("Item");

                b.HasKey(e => e.ItemId);

                b.Property(e => e.Name).IsRequired();
                b.Property(e => e.Address).IsRequired();
                b.Property(e => e.Latitude).IsRequired();
                b.Property(e => e.Longitude).IsRequired();

                b.Property(e => e.ConcurrencyStamp).IsConcurrencyToken();
            });


            builder.Seed();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {

            var sysAdminAppRole = ApplicationRoles.SystemAdministrator;
            var adminAppRole = ApplicationRoles.Administrator;

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = sysAdminAppRole.Id,
                    Name = sysAdminAppRole.Name,
                    NormalizedName = sysAdminAppRole.Name.ToUpper()
                },
                new IdentityRole
                {
                    Id = adminAppRole.Id,
                    Name = adminAppRole.Name,
                    NormalizedName = adminAppRole.Name.ToUpper()
                });

            builder.Entity<IdentityWebUser>().HasData(
                new IdentityWebUser
                {
                    Id = "systemadministrator1",
                    UserName = "systemadministrator1@web.com",
                    FirstName = "SysAdmin1 FirstName",
                    LastName = "SysAdmin1 LastName",
                    NormalizedUserName = "SYSTEMADMINISTRATOR1@WEB.COM",

                    Email = "systemadministrator1@web.com",
                    NormalizedEmail = "SYSTEMADMINISTRATOR1@WEB.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "09876543212",
                    PhoneNumberConfirmed = true,

                    LockoutEnabled = false,
                    LockoutEnd = null,
                    PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
                    SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
                    TwoFactorEnabled = false,
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "7af7285e-ba87-4f2b-b729-14e5b74bb8a1",
                },
                new IdentityWebUser
                {
                    Id = "systemadministrator2",
                    UserName = "systemadministrator2@web.com",
                    FirstName = "SysAdmin2 FirstName",
                    LastName = "SysAdmin2 LastName",
                    NormalizedUserName = "SYSTEMADMINISTRATOR2@WEB.COM",

                    Email = "systemadministrator2@web.com",
                    NormalizedEmail = "SYSTEMADMINISTRATOR2@WEB.COM",
                    EmailConfirmed = true,
                    PhoneNumber = "09876543212",
                    PhoneNumberConfirmed = true,

                    LockoutEnabled = false,
                    LockoutEnd = null,
                    PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
                    SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
                    TwoFactorEnabled = false,
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "7af7285e-ba87-4f2b-b729-14e5b74bb8a1",
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "systemadministrator1",
                    RoleId = sysAdminAppRole.Id
                },
                new IdentityUserRole<string>
                {
                    UserId = "systemadministrator2",
                    RoleId = sysAdminAppRole.Id
                });

            //muzon, poblacion, calumpang east, calumpang west, taliba, tunggal, tejero, bagong tubig, banoyo




            //  Barangays
            //  13.842046, 120.943854
            builder.Entity<Barangay>().HasData(
                new Barangay
                {
                    BarangayId = "Barangay1",
                    Name = "Muzon",
                    Description = "muzon",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay2",
                    Name = "Poblacion",
                    Description = "poblacion",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay3",
                    Name = "Calumpang East",
                    Description = "calumpang east",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay4",
                    Name = "Calumpang West",
                    Description = "calumpang west",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay5",
                    Name = "Taliba",
                    Description = "taliba",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay6",
                    Name = "Tunggal",
                    Description = "tunggal",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay7",
                    Name = "Tejero",
                    Description = "tejero",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay8",
                    Name = "Bagong Tubig",
                    Description = "bagong tubig",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                },
                new Barangay
                {
                    BarangayId = "Barangay9",
                    Name = "Banoyo",
                    Description = "Banoyo",
                    Latitude = 13.842046,
                    Longitude = 120.943854
                }
                );

            builder.Entity<Item>().HasData(
                new Item
                {
                    ItemId = "Barangay1Item1",
                    ItemType = EnumItemType.Camera,
                    ItemStatus = EnumItemStatus.Unknown,
                    BarangayId = "Barangay1",
                    UserId = "systemadministrator1",
                    Name = "Barangay #1 Item #1",
                    Description = "Barangay Number 1 Item Number 1",
                    Address = "Address 1",
                    Latitude = 13.842146,
                    Longitude = 120.923854
                },
                new Item
                {
                    ItemId = "Barangay1Item2",
                    ItemType = EnumItemType.Wifi,
                    ItemStatus = EnumItemStatus.On,
                    BarangayId = "Barangay1",
                    UserId = "systemadministrator1",
                    Name = "Barangay #1 Item #2",
                    Description = "Barangay Number 1 Item Number 2",
                    Address = "Address 2",
                    Latitude = 13.812346,
                    Longitude = 120.943854
                },
                new Item
                {
                    ItemId = "Barangay1Item3",
                    ItemType = EnumItemType.Camera,
                    ItemStatus = EnumItemStatus.Off,
                    BarangayId = "Barangay1",
                    UserId = "systemadministrator2",
                    Name = "Barangay #1 Item #3",
                    Description = "Barangay Number 1 Item Number 3",
                    Address = "Address 3",
                    Latitude = 13.862546,
                    Longitude = 120.943854
                },
                new Item
                {
                    ItemId = "Barangay2Item4",
                    ItemType = EnumItemType.Camera,
                    ItemStatus = EnumItemStatus.On,
                    BarangayId = "Barangay2",
                    UserId = "systemadministrator2",
                    Name = "Barangay #2 Item #4",
                    Description = "Barangay Number 2 Item Number 4",
                    Address = "Address 4",
                    Latitude = 13.862546,
                    Longitude = 120.963854
                });
        }
    }
}
