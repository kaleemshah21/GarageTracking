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
        private readonly IConfiguration Configuration;

        public IndexModel(GarageTracking.Data.TrackingContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

       

        


        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string LastNameSort { get; set; }
        public string FirstNameSort { get; set; }
        public PaginatedList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString,string currentFilter,int? pageIndex)
        {
            LastNameSort = sortOrder == "lname_asc" || String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "lname_asc";
            FirstNameSort = sortOrder == "fname_asc" || String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "fname_asc";


            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Customer> customersIQ = from s in _context.Customers
                                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                customersIQ = customersIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

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


            //Customer = await customersIQ.AsNoTracking().ToListAsync();

            var pageSize = Configuration.GetValue("PageSize", 4);
            Customer = await PaginatedList<Customer>.CreateAsync(
               customersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
