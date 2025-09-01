using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear the session on logout
            HttpContext.Session.Clear();

            // Redirect to home page after logout
            return RedirectToPage("/Index");
        }
    }
}