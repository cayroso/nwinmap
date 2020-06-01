using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWINMAP.Web.Areas.Identity.Data.App;
using NWINMAP.Web.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NWINMAP.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AdminController : ControllerBase
    {
        readonly IdentityWebContext _dbContext;
        public AdminController(IdentityWebContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet("items")]
        public async Task<IActionResult> GeItemsAsync()
        {
            var data = await _dbContext.Items
                .Include(e => e.Barangay)
                .Include(e => e.User)
                .AsNoTracking()
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GeItemsAsync(string id)
        {
            var data = await _dbContext.Items
                .Include(e => e.Barangay)
                .Include(e => e.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ItemId == id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet("barangays")]
        public async Task<IActionResult> GetBarangaysAsync()
        {
            var data = await _dbContext.Barangays
                .AsNoTracking()
                .Select(e => new
                {
                    Value = e.BarangayId,
                    Text = e.Name
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItemAsync([FromBody]AddItemInfo info)
        {
            var itemId = Guid.NewGuid().ToString();

            var data = new Item
            {
                ItemId = itemId,
                BarangayId = info.BarangayId,
                ItemType = info.ItemType,
                ItemStatus = info.ItemStatus,
                Name = info.Name,
                Description = info.Description,
                Latitude = info.Latitude,
                Longitude = info.Longitude,
                Address = info.Address,
                Image = info.Image,
                Source = info.Source,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await _dbContext.AddAsync(data);

            await _dbContext.SaveChangesAsync();

            return Ok(itemId);
        }

        [HttpPut("items")]
        public async Task<IActionResult> EditItemAsync([FromBody]EditItemInfo info)
        {
            var data = await _dbContext.Items.FirstOrDefaultAsync(e => e.ItemId == info.ItemId);

            if (data == null)
                return BadRequest("Item not found.");

            if (data.ConcurrencyStamp != info.ConcurrencyStamp)
                return BadRequest("Item already updated by another user.");


            data.ItemType = info.ItemType;
            data.ItemStatus = info.ItemStatus;
            data.Name = info.Name;
            data.Description = info.Description;
            data.Image = info.Image;
            data.Address = info.Address;
            data.Latitude = info.Latitude;
            data.Longitude = info.Longitude;
            data.Source = info.Source;

            data.ConcurrencyStamp = Guid.NewGuid().ToString();

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }

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
