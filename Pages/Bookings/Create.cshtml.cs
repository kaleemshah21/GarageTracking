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

namespace GarageTracking.Pages.Bookings
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
            ViewData["VehicleID"] = new SelectList(_context.Vehicles.Select(v => new {
                v.VehicleId,
                Registration = v.Registration
            }), "VehicleId", "Registration");


            ViewData["Status"] = Enum.GetValues(typeof(Booking.BookingStatus))
                .Cast<Booking.BookingStatus>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.ToString()
                })
                .ToList();

            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
