using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public void OnGet(string id)
        {
            Id = id;
            //Input.Latitude = 13.895885;
            //Input.Longitude = 120.906946;

            //if (!string.IsNullOrWhiteSpace(id))
            //{
            //    var data = await identity.Items.FirstOrDefaultAsync(e => e.ItemId == id);
            //    if (data != null)
            //    {
            //        Input = new Input
            //        {
            //            ItemId = data.ItemId,
            //            BarangayId = data.BarangayId,
            //            Name = data.Name,
            //            Description = data.Description,
            //            Image = data.Image,
            //            Source = data.Source,
            //            Address = data.Address,
            //            Latitude = data.Latitude,
            //            Longitude = data.Longitude
            //        };
            //    }
            //}

            //var barangays = await identity.Barangays
            //    .ToListAsync();

            //Barangays = barangays.Select(e => new SelectListItem(e.Name, e.BarangayId)).ToList();
        }

        //public async Task<IActionResult> OnPost([FromServices] IdentityWebContext identity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (string.IsNullOrWhiteSpace(Input.ItemId))
        //        {
        //            var item = new Item
        //            {
        //                ItemId = Guid.NewGuid().ToString(),
        //                BarangayId = Input.BarangayId,
        //                Name = Input.Name,
        //                Description = Input.Description,
        //                Image = Input.Image,
        //                Source = Input.Source,
        //                Address = Input.Address,
        //                Latitude = Input.Latitude,
        //                Longitude = Input.Longitude,
        //            };

        //            await identity.AddAsync(item);
        //        }
        //        else
        //        {
        //            var data = await identity.Items.FirstOrDefaultAsync(e => e.ItemId == Input.ItemId);

        //            if (data != null)
        //            {
        //                data.BarangayId = Input.BarangayId;
        //                data.Name = Input.Name;
        //                data.Description = Input.Description;
        //                data.Image = Input.Image;
        //                data.Source = Input.Source;
        //                data.Address = Input.Address;
        //                data.Latitude = Input.Latitude;
        //                data.Longitude = Input.Longitude;
        //            }
        //            else
        //            {
        //                throw new ApplicationException("Item not found.");
        //            }
        //        }

        //        await identity.SaveChangesAsync();

        //        return Redirect("/Index");
        //    }

        //    var barangays = await identity.Barangays
        //        .ToListAsync();

        //    Barangays = barangays.Select(e => new SelectListItem(e.Name, e.BarangayId)).ToList();

        //    return Page();
        //}
    }

    //public class Input
    //{
    //    public string ItemId { get; set; }
    //    [Required]
    //    public string BarangayId { get; set; }

    //    public string Image { get; set; }
    //    public string Source { get; set; }
    //    [Required]
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    [Required]
    //    public string Address { get; set; }
    //    [Required]
    //    public double Latitude { get; set; }
    //    [Required]
    //    public double Longitude { get; set; }
    //}
}