using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NWINMAP.Web.Areas.Identity.Data.App;
using NWINMAP.Web.Data;
using NWINMAP.Web.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWINMAP.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemController : BaseController
    {
        readonly IdentityWebContext _dbContext;
        public ItemController(IdentityWebContext dbContext, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
            : base(apiBehaviorOptions)

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
            var userBarangayIds = await _dbContext.BarangayUserRoles.Where(e => e.UserId == UserId).Select(e => e.BarangayId).ToListAsync();

            var data = await _dbContext.Barangays
                .AsNoTracking()
                .Where(e => userBarangayIds.Contains(e.BarangayId))
                .Select(e => new
                {
                    Value = e.BarangayId,
                    Text = e.Name
                })
                .ToListAsync();

            return Ok(data);
        }

        [Authorize]
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
                UserId = UserId
            };

            await _dbContext.AddAsync(data);

            await _dbContext.SaveChangesAsync();

            return Ok(itemId);
        }

        [Authorize]
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
}
