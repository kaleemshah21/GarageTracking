using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Data;
using GarageTracking.Models;
using Microsoft.AspNetCore.Authorization;

namespace GarageTracking.Pages.Bookings
{
    [Authorize(Policy = "RequireUserRole")]
    public class EditModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public EditModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking =  await _context.Bookings.FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }
            Booking = booking;
            // Get the list of vehicles
            var vehicles = await _context.Vehicles.ToListAsync();

            // Create a list of SelectListItems and add a null option
            var vehicleSelectList = new List<SelectListItem>
            {
            new SelectListItem { Value = "", Text = "Select a Vehicle" } // Placeholder for null option
            };

            // Add the vehicles to the list
            vehicleSelectList.AddRange(vehicles.Select(v => new SelectListItem
            {
                Value = v.VehicleId.ToString(),
                Text = v.Registration // Display vehicle registration
            }));

            // Assign the new list to ViewData
            ViewData["VehicleID"] = vehicleSelectList;




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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Booking).State = EntityState.Modified;

            try
            {
                if (Booking.Status == Booking.BookingStatus.Done)
                {
                    // Checks if an invoice already exists for this booking
                    bool invoiceExists = await _context.Invoices.AnyAsync(i => i.BookingID == Booking.BookingID);
                    if (!invoiceExists)
                    {
                        // Creates an invoice for the booking
                        var invoice = new Invoice
                        {
                            BookingID = Booking.BookingID,
                            InvoiceDate = DateTime.Now,
                            Booking = Booking
                        };

                        // adds the invoice
                        _context.Invoices.Add(invoice);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(Booking.BookingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
        }
    }
}
