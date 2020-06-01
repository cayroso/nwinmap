using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NWINMAP.Web.Areas.Identity.Data.App;
using NWINMAP.Web.Data;

namespace NWINMAP.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Barangay> Barangays { get; set; }
        public List<Item> Items { get; set; }

        public async Task OnGet([FromServices]IdentityWebContext identity)
        {
            Items = await identity.Items
                .Include(e => e.Barangay)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
