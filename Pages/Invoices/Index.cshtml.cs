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
using Microsoft.Data.SqlClient;

namespace GarageTracking.Pages.Invoices
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
        public string InvoiceDateSort { get; set; }
        public string BookingIDSort { get; set; }
        public string CustomerSort { get; set; }
        public string VehicleRegistrationSort { get; set; }
        public string ServiceDateSort { get; set; }
        public string ServiceTypeSort { get; set; }

        public PaginatedList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {

            CurrentSort = sortOrder;
            InvoiceDateSort = sortOrder == "invoice_date_asc" ? "invoice_date_desc" : "invoice_date_asc";
            BookingIDSort = sortOrder == "booking_id_asc" ? "booking_id_desc" : "booking_id_asc";
            CustomerSort = sortOrder == "customer_asc" ? "customer_desc" : "customer_asc";
            VehicleRegistrationSort = sortOrder == "vehicle_registration_asc" ? "vehicle_registration_desc" : "vehicle_registration_asc";
            ServiceDateSort = sortOrder == "service_date_asc" ? "service_date_desc" : "service_date_asc";
            ServiceTypeSort = sortOrder == "service_type_asc" ? "service_type_desc" : "service_type_asc";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Invoice> invoicesIQ = from i in _context.Invoices
                                              .Include(i => i.Booking)
                                              .ThenInclude(b => b.Vehicle)
                                              .ThenInclude(v => v.Customer)
                                             select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                invoicesIQ = invoicesIQ.Where(s => s.Booking.Vehicle.Customer.LastName.Contains(searchString)
                                       || s.Booking.Vehicle.Customer.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "invoice_date_asc":
                    invoicesIQ = invoicesIQ.OrderBy(i => i.InvoiceDate);
                    break;
                case "invoice_date_desc":
                    invoicesIQ = invoicesIQ.OrderByDescending(i => i.InvoiceDate);
                    break;
                case "booking_id_asc":
                    invoicesIQ = invoicesIQ.OrderBy(i => i.Booking.BookingID);
                    break;
                case "booking_id_desc":
                    invoicesIQ = invoicesIQ.OrderByDescending(i => i.Booking.BookingID);
                    break;
                case "customer_asc":
                    invoicesIQ = invoicesIQ.OrderBy(i => i.Booking.Vehicle.Customer.LastName).ThenBy(i => i.Booking.Vehicle.Customer.FirstName);
                    break;
                case "customer_desc":
                    invoicesIQ = invoicesIQ.OrderByDescending(i => i.Booking.Vehicle.Customer.LastName).ThenByDescending(i => i.Booking.Vehicle.Customer.FirstName);
                    break;
                case "vehicle_registration_asc":
                    invoicesIQ = invoicesIQ.OrderBy(i => i.Booking.Vehicle.Registration);
                    break;
                case "vehicle_registration_desc":
                    invoicesIQ = invoicesIQ.OrderByDescending(i => i.Booking.Vehicle.Registration);
                    break;
                case "service_date_asc":
                    invoicesIQ = invoicesIQ.OrderBy(i => i.Booking.ServiceDate);
                    break;
                case "service_date_desc":
                    invoicesIQ = invoicesIQ.OrderByDescending(i => i.Booking.ServiceDate);
                    break;
                case "service_type_asc":
                    invoicesIQ = invoicesIQ.OrderBy(i => i.Booking.ServiceType);
                    break;
                case "service_type_desc":
                    invoicesIQ = invoicesIQ.OrderByDescending(i => i.Booking.ServiceType);
                    break;
                default:
                    invoicesIQ = invoicesIQ.OrderBy(i => i.InvoiceDate); // Default sorting by InvoiceDate
                    break;
            }


            //Invoice = await invoicesIQ.AsNoTracking().ToListAsync();
            var pageSize = Configuration.GetValue("PageSize", 4);
            Invoice = await PaginatedList<Invoice>.CreateAsync(
               invoicesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
