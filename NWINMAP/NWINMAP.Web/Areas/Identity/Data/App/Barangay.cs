using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWINMAP.Web.Areas.Identity.Data.App
{
    public class Barangay
    {
        public string BarangayId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
        public virtual ICollection<BarangayUserRole> UserRoles { get; set; } = new List<BarangayUserRole>();

        public string ConcurrencyStamp { get; set; }
    }

    public class BarangayUserRole
    {
        public string BarangayId { get; set; }
        public virtual Barangay Barangay { get; set; }

        public string UserId { get; set; }
        public virtual IdentityWebUser User { get; set; }

        public string RoleId { get; set; }
        public virtual IdentityRole Role { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
