using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    public class AdminController : BaseController
    {
        readonly IdentityWebContext _dbContext;
        public AdminController(IdentityWebContext dbContext, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
            : base(apiBehaviorOptions)

        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var data = await _dbContext.Users
                        //.Where(e=>e.userr)
                        .ToListAsync();
            return Ok();
        }
    }

    

}
