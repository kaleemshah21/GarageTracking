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

namespace GarageTracking.Pages.Vehicles
{
    [Authorize(Policy = "RequireUserRole")]
    public class CreateModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public CreateModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers.Select(c => new {
                c.CustomerID,
                FullName = c.FirstName + " " + c.LastName
            }), "CustomerID", "FullName");
            return Page();
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Vehicles.Add(Vehicle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
