using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NWINMAP.Web.Areas.Identity.Data.App;
using NWINMAP.Web.Data;

namespace NWINMAP.Web
{
    [Authorize]
    public class AddEditItemModel : PageModel
    {
        public string Id { get; set; }
        //[BindProperty]
        //public Input Input { get; set; } = new Input();

        public List<SelectListItem> Barangays { get; set; }

        public async Task<IActionResult> OnGet([FromServices]IdentityWebContext dbContext, string id)
        {
            Id = id;

            if (!string.IsNullOrWhiteSpace(id))
            {
                var data = await dbContext.Items.FirstOrDefaultAsync(e => e.ItemId == id);

                if (data == null)
                {
                    return NotFound("Item not found.");
                }

                //  get allowed barangays for current User
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var barangays = await dbContext.BarangayUserRoles.Where(e => e.UserId == userId).Select(e => e.BarangayId).ToListAsync();

                if (!barangays.Contains(data.BarangayId))
                {
                    return RedirectToPage("/NotAuthorizedToEditDevice");
                }
            }

            return Page();
        }

    }

}