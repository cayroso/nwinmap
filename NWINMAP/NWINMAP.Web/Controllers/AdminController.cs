using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NWINMAP.Web.Areas.Identity.Constants;
using NWINMAP.Web.Areas.Identity.Data;
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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AdminController : BaseController
    {
        readonly IdentityWebContext _dbContext;
        readonly UserManager<IdentityWebUser> _userManager;
        public AdminController(IdentityWebContext dbContext,
            UserManager<IdentityWebUser> userManager,
            IOptions<ApiBehaviorOptions> apiBehaviorOptions)
            : base(apiBehaviorOptions)

        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(string c)
        {
            var sql = from user in _dbContext.Users.AsNoTracking()

                      join ur in _dbContext.UserRoles.AsNoTracking() on user.Id equals ur.UserId into urs
                      from ur in urs.DefaultIfEmpty()

                      join r in _dbContext.Roles.AsNoTracking() on ur.RoleId equals r.Id into rs
                      from r in rs.DefaultIfEmpty()

                          //join bur in _dbContext.BarangayUserRoles on user.Id equals bur.UserId into burs
                          //from bur in burs.DefaultIfEmpty()

                          //join r2 in _dbContext.Roles on bur.RoleId equals r2.Id into rs2
                          //from r2 in rs2.DefaultIfEmpty()

                          //join b in _dbContext.Barangays on bur.BarangayId equals b.BarangayId into bs
                          //from b in bs.DefaultIfEmpty()
                      where string.IsNullOrWhiteSpace(c)
                            || EF.Functions.Like(user.FirstName, $"%{c}%")
                            || EF.Functions.Like(user.LastName, $"%{c}%")
                            || EF.Functions.Like(user.Email, $"%{c}%")
                            || EF.Functions.Like(user.PhoneNumber, $"%{c}%")


                      select new
                      {
                          User = new
                          {
                              user.Id,
                              user.FirstName,
                              user.LastName,
                              user.Email,
                              user.PhoneNumber
                          },
                          Role = new { r.Id, r.Name },
                          //BarangayUserRole = new
                          //{
                          //    bur.BarangayId,
                          //    BarangayName = bur.Barangay.Name,
                          //    bur.RoleId,
                          //    RoleName = bur.Role.Name
                          //}
                      };

            var query = await sql.ToListAsync();

            var data = query.GroupBy(e => e.User).Select(e => new UserInfo
            {
                UserId = e.Key.Id,
                FirstName = e.Key.FirstName,
                LastName = e.Key.LastName,
                Email = e.Key.Email,
                Phone = e.Key.PhoneNumber,
                Roles = e.Where(e => !string.IsNullOrWhiteSpace(e.Role.Id)).Select(r => new RoleInfo
                {
                    RoleId = r.Role.Id,
                    Name = r.Role.Name
                }).Distinct().ToList(),
            }).ToList();

            var bur = await _dbContext.BarangayUserRoles.Include(e => e.Barangay).Include(e => e.Role).AsNoTracking().ToListAsync();

            data.ForEach(user =>
            {
                user.BarangayUserRoles = bur.Where(e => e.UserId == user.UserId).Select(e => new BarangayUserRoleInfo
                {
                    BarangayId = e.BarangayId,
                    BarangayName = e.Barangay.Name,
                    RoleId = e.RoleId,
                    RoleName = e.Role.Name
                }).ToList();
            });

            return Ok(data);
        }

        [HttpGet("users/{uid}/barangays")]
        public async Task<IActionResult> GetUserBarangays(string uid)
        {
            var data = await _dbContext.BarangayUserRoles
                            .Include(e => e.Barangay)
                            .Include(e => e.Role)
                            .AsNoTracking()
                            .Where(e => e.UserId == uid)
                            .Select(e => new BarangayUserRoleInfo
                            {
                                BarangayId = e.BarangayId,
                                BarangayName = e.Barangay.Name,
                                RoleId = e.RoleId,
                                RoleName = e.Role.Name
                            })
                            .ToListAsync();


            return Ok(data);
        }

        [HttpPost("manage-user-role")]
        public async Task<IActionResult> ManageUserRole([FromBody]ManageUserRoleInfo info)
        {
            var urToRemove = await _dbContext.UserRoles.Where(e => e.UserId == info.UserId && info.RoleIdsRemove.Contains(e.RoleId)).ToListAsync();

            var urToAdd = info.RoleIdsAdd.Select(e => new IdentityUserRole<string>
            {
                UserId = info.UserId,
                RoleId = e
            }).ToList();

            _dbContext.RemoveRange(urToRemove);

            await _dbContext.AddRangeAsync(urToAdd);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("manage-user-barangay")]
        public async Task<IActionResult> ManageUserBarangay([FromBody]ManageUserBarangayInfo info)
        {
            var urToRemove = await _dbContext.BarangayUserRoles
                                    .Where(e => e.UserId == info.UserId && info.BarangayIdsRemove.Contains(e.BarangayId)).ToListAsync();

            var urToAdd = info.BarangayIdsAdd.Select(e => new BarangayUserRole
            {
                UserId = info.UserId,
                BarangayId = e,
                RoleId = ApplicationRoles.User.Id
            }).ToList();

            _dbContext.RemoveRange(urToRemove);

            await _dbContext.AddRangeAsync(urToAdd);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var data = await _dbContext.Roles.AsNoTracking()
                            .Select(e => new { RoleId = e.Id, e.Name }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("barangays")]
        public async Task<IActionResult> GetBarangays()
        {
            var data = await _dbContext.Barangays.AsNoTracking()
                            .Select(e => new { e.BarangayId, e.Name }).ToListAsync();

            return Ok(data);
        }

    }

    public class ManageUserRoleInfo
    {
        public string UserId { get; set; }
        public List<string> RoleIdsAdd { get; set; }
        public List<string> RoleIdsRemove { get; set; }
    }

    public class ManageUserBarangayInfo
    {
        public string UserId { get; set; }
        public List<string> BarangayIdsAdd { get; set; }
        public List<string> BarangayIdsRemove { get; set; }
    }

    public class UserInfo
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<RoleInfo> Roles { get; set; } = new List<RoleInfo>();

        public List<BarangayUserRoleInfo> BarangayUserRoles { get; set; } = new List<BarangayUserRoleInfo>();
    }

    public class RoleInfo
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
    }

    public class BarangayUserRoleInfo
    {
        public string BarangayId { get; set; }
        public string BarangayName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

}
