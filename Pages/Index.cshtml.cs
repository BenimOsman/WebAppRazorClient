using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppRazorSandwitchClient.Pages                                              // This is the Index page model class for handling the default page (Home).
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;                     // Declare the IHttpContextAccessor to access the current HTTP context.

        public IndexModel(IHttpContextAccessor httpContextAccessor)                     // Constructor that injects IHttpContextAccessor for accessing the HTTP context.
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserEmail { get; set; }                                          // Public property to store the user email retrieved from session.

        public IActionResult OnGet()                                                    // This is the OnGet method which handles the GET request for this page.
        {
            UserEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");    // Get the "UserEmail" from the session.

            if (string.IsNullOrEmpty(UserEmail))                                            // Check if the session does not contain the user email (i.e., the user is not logged in).
            {
                return RedirectToPage("/Account/Login");                                // Redirect to the Login page if no user email is found in session.
            }

            return Page();                                                              // If the session contains the user email, return the current page.
        }
    }
}