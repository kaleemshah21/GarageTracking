using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageTracking.Data;
using GarageTracking.Models;
using Microsoft.AspNetCore.Authorization;

namespace GarageTracking.Pages.Users
{
    [Authorize(Policy = "RequireAdminRole")]
    public class EditModel : PageModel
    {
        private readonly GarageTracking.Data.TrackingContext _context;

        public EditModel(GarageTracking.Data.TrackingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;
        public SelectList RoleSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user =  await _context.Users.FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            RoleSelectList = new SelectList(new List<string> { "Admin", "User" });
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                RoleSelectList = new SelectList(new List<string> { "Admin", "User" });
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
