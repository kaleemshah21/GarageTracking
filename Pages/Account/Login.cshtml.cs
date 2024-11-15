using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using GarageTracking.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GarageTracking.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly TrackingContext _context;

        public LoginModel(TrackingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        // holds alert message
        [TempData]
        public string AlertMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                AlertMessage = "Invalid login attempt.";
                return Page();
            }

            
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == Username);

            if (user != null && Password == user.Password)
            {
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                
                return RedirectToPage("/Index");
            }

            AlertMessage = "Invalid login attempt.";
            return Page();
        }
    }
}
