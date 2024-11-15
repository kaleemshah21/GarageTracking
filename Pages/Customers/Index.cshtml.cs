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

namespace GarageTracking.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;


        public IndexModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        public string CurrentSort { get; set; }
        public string LastNameSort { get; set; }
        public string FirstNameSort { get; set; }
        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            LastNameSort = sortOrder == "lname_asc" || String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "lname_asc";
            FirstNameSort = sortOrder == "fname_asc" || String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "fname_asc";

            IQueryable<Customer> customersIQ = from s in _context.Customers
                                               select s;

            switch (sortOrder)
            {
                case "lname_asc":
                    customersIQ = customersIQ.OrderBy(c => c.LastName);
                    break;
                case "lname_desc":
                    customersIQ = customersIQ.OrderByDescending(c => c.LastName);
                    break;
                case "fname_asc":
                    customersIQ = customersIQ.OrderBy(c => c.FirstName);
                    break;
                case "fname_desc":
                    customersIQ = customersIQ.OrderByDescending(c => c.FirstName);
                    break;
                default:
                    customersIQ = customersIQ.OrderBy(c => c.LastName); // Default sorting
                    break;
            }


            Customer = await customersIQ.AsNoTracking().ToListAsync();
        }
    }
}
