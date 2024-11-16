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

        private readonly IConfiguration Configuration;

        public IndexModel(GarageTracking.Data.TrackingContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string ServiceTypeSort { get; set; }
        public string ServiceDateSort { get; set; }
        public string PriceSort { get; set; }
        public string StatusSort { get; set; }
        public string RegistrationSort { get; set; }
        public string CustomerNameSort { get; set; }

        public PaginatedList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {

            CurrentSort = sortOrder;
            ServiceTypeSort = sortOrder == "service_type_asc" ? "service_type_desc" : "service_type_asc";
            ServiceDateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            PriceSort = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            StatusSort = sortOrder == "status_asc" ? "status_desc" : "status_asc";
            RegistrationSort = sortOrder == "registration_asc" ? "registration_desc" : "registration_asc";
            CustomerNameSort = sortOrder == "customer_name_asc" ? "customer_name_desc" : "customer_name_asc";
            

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            //only gets the bookings with status in progress or waiting
            IQueryable<Booking> bookingsIQ = from b in _context.Bookings
                                     .Include(b => b.Vehicle)
                                     .ThenInclude(v => v.Customer)
                                             where b.Status == GarageTracking.Models.Booking.BookingStatus.Waiting ||
                                                   b.Status == GarageTracking.Models.Booking.BookingStatus.InProgress
                                             select b;


            if (!String.IsNullOrEmpty(searchString))
            {
                bookingsIQ = bookingsIQ.Where(s => s.Vehicle.Customer.LastName.Contains(searchString)
                                       || s.Vehicle.Customer.FirstName.Contains(searchString));
            }

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
                case "customer_name_asc":
                    bookingsIQ = bookingsIQ.OrderBy(b => b.Vehicle.Customer.FirstName + " " + b.Vehicle.Customer.LastName);
                    break;
                case "customer_name_desc":
                    bookingsIQ = bookingsIQ.OrderByDescending(b => b.Vehicle.Customer.FirstName + " " + b.Vehicle.Customer.LastName);
                    break;
                default:
                    bookingsIQ = bookingsIQ.OrderBy(b => b.ServiceDate); // Default sorting by ServiceDate
                    break;
            }


            //Booking = await bookingsIQ.AsNoTracking().ToListAsync();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Booking = await PaginatedList<Booking>.CreateAsync(
               bookingsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
