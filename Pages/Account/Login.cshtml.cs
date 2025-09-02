using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WebAppRazorSandwitchClient;                                                           // Contains LoginViewModel and AuthService

namespace WebAppRazorSandwitchClient.Pages.Account
{
    public class LoginModel : PageModel                                                     // This is the Login page model class.
    {
        private readonly AuthService _authService;                                          // Declare an instance of AuthService to handle login logic

        public LoginModel(AuthService authService)                                          // Constructor that injects AuthService for handling login functionality
        {
            _authService = authService;                                                     // Initialize AuthService
        }

        [BindProperty]                                                                      // To capture the login details from the user
        public LoginViewModel Input { get; set; } = new LoginViewModel();                   // Holds user input (email and password)


        // Optional property to hold an error message if login fails.
        public string? ErrorMessage { get; set; }                                           // Holds error message 

        public void OnGet()                                                                 // Handles GET requests for this page
        {
            // Optional: If the user is already logged in (session exists), redirect them to the Index page.
            if (HttpContext.Session.GetString("IsLoggedIn") == "true") // Check if user is already logged in
            {
                Response.Redirect("/Index");                                                // Redirect the user to the Index page
            }
        }

        // Handles POST request when the form is submitted
        public async Task<IActionResult> OnPostAsync()                                      // Handles POST request when the form is submitted.
        {
            // Return the page if the model is invalid 
            if (!ModelState.IsValid)
                return Page();

            // Attempt login with AuthService
            var success = await _authService.LoginAsync(Input);                             // Call AuthService to login.

            if (success)                                                                    // If login is successful
            {
                // Set session values indicating the user is logged in
                HttpContext.Session.SetString("IsLoggedIn", "true");                        // Mark as logged in    - Stores the value as 'True' under the IsLoggedIn
                HttpContext.Session.SetString("UserEmail", Input.Email);                    // Store user email     - Stores the user's email under the key "UserEmail"

                return RedirectToPage("/Index");                                            // Redirect to the Index page after login.
            }

            ErrorMessage = "Invalid login attempt.";                                        // If login fails, set error message and redisplay the page.
            return Page();                                                                  // Return to login page with error.
        }
    }
}