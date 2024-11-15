using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Data;
using GarageTracking.Models;
using Microsoft.AspNetCore.Authorization;

namespace GarageTracking.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public IndexModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync()
        {

            Invoice = await _context.Invoices
                .Include(i => i.Booking)
                    .ThenInclude(b => b.Vehicle)
                        .ThenInclude(v => v.Customer)
                .ToListAsync();

        }
    }
}
