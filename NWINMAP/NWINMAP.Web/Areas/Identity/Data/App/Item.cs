using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWINMAP.Web.Areas.Identity.Data.App
{
    public enum EnumItemType
    {
        Unknown = 0,
        Camera = 1,
        Wifi = 2
    }

    public enum EnumItemStatus
    {
        Unknown = -1,
        Off = 0,
        On = 1,
    }

    public class Item
    {
        public string ItemId { get; set; }

        public string BarangayId { get; set; }
        public virtual Barangay Barangay { get; set; }

        public string UserId { get; set; }
        public virtual IdentityWebUser User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public EnumItemType ItemType { get; set; } = EnumItemType.Unknown;
        public EnumItemStatus ItemStatus { get; set; } = EnumItemStatus.Unknown;
        public string ItemTypeText => ItemType.ToString();
        public string ItemStatusText => ItemStatus.ToString();
        public string Source { get; set; }
        public string Image { get; set; }

        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
