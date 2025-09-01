using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace WebAppRazorSandwitchClient.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginModel(AuthService authSesrvice)
        {
            _authService = authSesrvice;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; } = new LoginViewModel();

        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var success = await _authService.LoginAsync(Input);

            if (success)
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.SetString("UserEmail", Input.Email);
                return RedirectToPage("/Index");
            }
            ErrorMessage = "Invalid login attempt.";
            return Page();
        }
    }
}