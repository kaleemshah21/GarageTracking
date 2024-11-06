using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Data;
using GarageTracking.Models;

namespace GarageTracking.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public IndexModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //only gets the bookings with status in progress or waiting
            Booking = await _context.Bookings
            .Include(b => b.Vehicle)
            .Where(b => b.Status == GarageTracking.Models.Booking.BookingStatus.Waiting ||
                    b.Status == GarageTracking.Models.Booking.BookingStatus.InProgress)
            .ToListAsync();
        }
    }
}
