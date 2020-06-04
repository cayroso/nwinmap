using NWINMAP.Web.Areas.Identity.Data.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NWINMAP.Web.Models.Items
{
    public class AddItemInfo
    {
        [Required]
        public string BarangayId { get; set; }
        [Required]
        public EnumItemType ItemType { get; set; }
        [Required]
        public EnumItemStatus ItemStatus { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string Source { get; set; }

    }

    public class EditItemInfo : AddItemInfo
    {
        [Required]
        public string ItemId { get; set; }
        [Required]
        public string ConcurrencyStamp { get; set; }
    }
}
