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

namespace GarageTracking.Pages.Invoices
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
        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice =  await _context.Invoices.FirstOrDefaultAsync(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return NotFound();
            }
            Invoice = invoice;
           ViewData["BookingID"] = new SelectList(_context.Bookings, "BookingID", "BookingID");
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

            _context.Attach(Invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(Invoice.InvoiceID))
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

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.InvoiceID == id);
        }
    }
}
