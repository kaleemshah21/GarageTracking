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

namespace GarageTracking.Pages.Users
{
    [Authorize(Policy = "RequireAdminRole")]
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
        public string UserSort { get; set; }


        public PaginatedList<User> User { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            UserSort = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "user_asc";
            CurrentSort = sortOrder;

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }





            IQueryable<User> usersQuery = _context.Users.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                usersQuery = usersQuery.Where(u => u.Username.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "user_desc":
                    usersQuery = usersQuery.OrderByDescending(u => u.Username);
                    break;
                default:
                    usersQuery = usersQuery.OrderBy(u => u.Username);
                    break;
            }

            //User = await usersQuery.AsNoTracking().ToListAsync();
            var pageSize = Configuration.GetValue("PageSize", 4);
            User = await PaginatedList<User>.CreateAsync(
               usersQuery.AsNoTracking(), pageIndex ?? 1, pageSize);

        }
    }
}
