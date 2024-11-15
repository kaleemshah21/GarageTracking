using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Data;
using GarageTracking.Models;

namespace GarageTracking.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public IndexModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }


        public string CurrentSort { get; set; }
        public string CarMakeSort { get; set; }
        public string CarModelSort { get; set; }
        public string RegistrationSort { get; set; }
        public string CustomerSort { get; set; }

        public IList<Vehicle> Vehicle { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            CarMakeSort = sortOrder == "car_make_asc" || String.IsNullOrEmpty(sortOrder) ? "car_make_desc" : "car_make_asc";
            CarModelSort = sortOrder == "car_model_asc" || String.IsNullOrEmpty(sortOrder) ? "car_model_desc" : "car_model_asc";
            RegistrationSort = sortOrder == "registration_asc" || String.IsNullOrEmpty(sortOrder) ? "registration_desc" : "registration_asc";
            CustomerSort = sortOrder == "customer_asc" || String.IsNullOrEmpty(sortOrder) ? "customer_desc" : "customer_asc";

            IQueryable<Vehicle> vehiclesIQ = from v in _context.Vehicles
                                         .Include(v => v.Customer)
                                             select v;

            switch (sortOrder)
            {
                case "car_make_asc":
                    vehiclesIQ = vehiclesIQ.OrderBy(v => v.CarMake);
                    break;
                case "car_make_desc":
                    vehiclesIQ = vehiclesIQ.OrderByDescending(v => v.CarMake);
                    break;
                case "car_model_asc":
                    vehiclesIQ = vehiclesIQ.OrderBy(v => v.CarModel);
                    break;
                case "car_model_desc":
                    vehiclesIQ = vehiclesIQ.OrderByDescending(v => v.CarModel);
                    break;
                case "registration_asc":
                    vehiclesIQ = vehiclesIQ.OrderBy(v => v.Registration);
                    break;
                case "registration_desc":
                    vehiclesIQ = vehiclesIQ.OrderByDescending(v => v.Registration);
                    break;
                case "customer_asc":
                    vehiclesIQ = vehiclesIQ.OrderBy(v => v.Customer.FirstName);
                    break;
                case "customer_desc":
                    vehiclesIQ = vehiclesIQ.OrderByDescending(v => v.Customer.FirstName);
                    break;
                default:
                    vehiclesIQ = vehiclesIQ.OrderBy(v => v.CarMake); // Default sorting by CarMake
                    break;
            }

            Vehicle = await vehiclesIQ.AsNoTracking().ToListAsync();
        }
    }
}
