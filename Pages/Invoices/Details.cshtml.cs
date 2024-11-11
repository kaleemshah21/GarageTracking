using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Data;
using GarageTracking.Models;

namespace GarageTracking.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public DetailsModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Invoice = await _context.Invoices
                .Include(i => i.Booking)
                    .ThenInclude(b => b.Vehicle)
                    .ThenInclude(v => v.Customer)
                .FirstOrDefaultAsync(m => m.InvoiceID == id);

            if (Invoice == null)
            {
                return NotFound();
            }
            else
            {
                Invoice = Invoice;
            }
            return Page();
        }
    }
}
