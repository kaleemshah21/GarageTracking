using GarageTracking.Data;
using GarageTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GarageTracking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TrackingContext _context;

        public int TotalActiveBookings { get; set; }
        public int ServicesCompletedToday { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalCustomers { get; set; }

        public List<Booking> NewestBookings { get; set; }

        public IndexModel(ILogger<IndexModel> logger, TrackingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            
            TotalActiveBookings = await _context.Bookings
                                                .Where(b => b.Status == Booking.BookingStatus.Waiting ||
                                                            b.Status == Booking.BookingStatus.InProgress)
                                                .CountAsync();

           
            ServicesCompletedToday = await _context.Invoices
                                                   .Where(i => i.InvoiceDate.Date == DateTime.Today)
                                                   .CountAsync();

           
            TotalSales = await _context.Invoices
                                       .SumAsync(i => i.Booking.Price);

           
            TotalCustomers = await _context.Customers.CountAsync();

            NewestBookings = await _context.Bookings
                .Where(b => b.ServiceDate.Date >= DateTime.Today && b.Status != Booking.BookingStatus.Done)
                .OrderBy(b => b.ServiceDate)
                .Take(4)
                .Include(b => b.Vehicle)
                    .ThenInclude(v => v.Customer)
                .ToListAsync();

            return Page();
        }
    }
}
