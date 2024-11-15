using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GarageTracking.Data;
using GarageTracking.Models;
using Microsoft.AspNetCore.Authorization;

namespace GarageTracking.Pages.Users
{
    [Authorize(Policy = "RequireAdminRole")]
    public class CreateModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public CreateModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> RoleList { get; set; }

        public IActionResult OnGet()
        {
            RoleList = new List<SelectListItem>
            {
                new SelectListItem { Value = "User", Text = "User" },
                new SelectListItem { Value = "Admin", Text = "Admin" }
            };
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("User.Role", "Please select a valid role.");
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
