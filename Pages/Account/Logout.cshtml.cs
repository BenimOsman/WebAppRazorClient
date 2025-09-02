using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()                                                        // Handles GET requests for the login page
        {
            // Clear the session data to log user out
            HttpContext.Session.Clear();               
            return RedirectToPage("/Account/Login");                                        // Redirect to login page
        }
    }
}