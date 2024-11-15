using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Data;
using GarageTracking.Models;
using Microsoft.Data.SqlClient;

namespace GarageTracking.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public IndexModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        public string CurrentSort { get; set; }
        public string ServiceTypeSort { get; set; }
        public string ServiceDateSort { get; set; }
        public string PriceSort { get; set; }
        public string StatusSort { get; set; }
        public string RegistrationSort { get; set; }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {

            CurrentSort = sortOrder;
            ServiceTypeSort = sortOrder == "service_type_asc" ? "service_type_desc" : "service_type_asc";
            ServiceDateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            PriceSort = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc";
            RegistrationSort = sortOrder == "registration_asc" ? "registration_desc" : "registration_asc";

            //only gets the bookings with status in progress or waiting
            IQueryable<Booking> bookingsIQ = from b in _context.Bookings
                                     .Include(b => b.Vehicle)
                                     .ThenInclude(v => v.Customer)
                                             where b.Status == GarageTracking.Models.Booking.BookingStatus.Waiting ||
                                                   b.Status == GarageTracking.Models.Booking.BookingStatus.InProgress
                                             select b;

            switch (sortOrder)
            {
                case "service_type_asc":
                    bookingsIQ = bookingsIQ.OrderBy(b => b.ServiceType);
                    break;
                case "service_type_desc":
                    bookingsIQ = bookingsIQ.OrderByDescending(b => b.ServiceType);
                    break;
                case "date_asc":
                    bookingsIQ = bookingsIQ.OrderBy(b => b.ServiceDate);
                    break;
                case "date_desc":
                    bookingsIQ = bookingsIQ.OrderByDescending(b => b.ServiceDate);
                    break;
                case "price_asc":
                    bookingsIQ = bookingsIQ.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    bookingsIQ = bookingsIQ.OrderByDescending(b => b.Price);
                    break;
                case "status_asc":
                    bookingsIQ = bookingsIQ.OrderBy(b => b.Status);
                    break;
                case "status_desc":
                    bookingsIQ = bookingsIQ.OrderByDescending(b => b.Status);
                    break;
                case "registration_asc":
                    bookingsIQ = bookingsIQ.OrderBy(b => b.Vehicle.Registration);
                    break;
                case "registration_desc":
                    bookingsIQ = bookingsIQ.OrderByDescending(b => b.Vehicle.Registration);
                    break;
                default:
                    bookingsIQ = bookingsIQ.OrderBy(b => b.ServiceDate); // Default sorting by ServiceDate
                    break;
            }


            Booking = await bookingsIQ.AsNoTracking().ToListAsync();
        }
    }
}
