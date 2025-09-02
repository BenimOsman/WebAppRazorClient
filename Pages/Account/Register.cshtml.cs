using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorSandwitchClient.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AuthService _authService;                                      // Declare an instance of AuthService

        public RegisterModel(AuthService authService)                                   // Constructor for injecting AuthService for handling registration functionality
        {
            _authService = authService;                                                 // Initialize AuthService
        }

        [BindProperty]                                                                  // To capture the registration details (email, password, etc.) from the user
        public RegisterViewModel Input { get; set; } = new RegisterViewModel();         // Holds user input for registration

        // If registration fails
        public string? ErrorMessage { get; set; }                                       // Holds error message if registration fails

        public void OnGet() { }                                                         // Empty, no special logic on GET request

        // Handles POST request for user registration.
        public async Task<IActionResult> OnPostAsync()
        {
            // Validate the model state (form data).
            if (!ModelState.IsValid)
                return Page(); // Return page with validation errors.

            // Attempt registration using AuthService.
            var success = await _authService.RegisterAsync(Input);

            // Redirect to Login page if successful.
            if (success)
            {
                return RedirectToPage("Login");
            }

            // Set error message if registration fails.
            ErrorMessage = "Registration failed. Try again.";
            return Page();                                                              // Return page with error message.
        }
    }
}