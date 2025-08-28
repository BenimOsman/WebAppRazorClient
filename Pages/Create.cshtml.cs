using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppRazorClient.Pages
{
    public class CreateModel : PageModel
    {
        private readonly SandwichService _service;

        public CreateModel(SandwichService service)
        {
            _service = service;
        }

        [BindProperty]
        public SandwichModel Sandwich { get; set; }

        public void OnGet()
        {
            // No initialization needed for a new sandwich
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Add the sandwich via the service
                var createdSandwich = await _service.AddSandwich(Sandwich);

                // Redirect to the sandwich list page after creation
                return RedirectToPage("/SwList");
            }
            catch (Exception ex)
            {
                // Optionally add error handling/logging
                ModelState.AddModelError(string.Empty, "Error creating sandwich: " + ex.Message);
                return Page();
            }
        }
    }
}