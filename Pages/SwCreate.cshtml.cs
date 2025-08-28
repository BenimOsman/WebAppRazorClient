using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazorClient;


namespace WebAppRazorClient.Pages
{
    public class SwCreateModel : PageModel
    {
        private readonly SandwichService _service;

        public SwCreateModel(SandwichService service)
        {
            _service = service;
        }

        [BindProperty]
        public SandwichModel NewSandwitch { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdSandwitch = await _service.AddSandwitch(NewSandwitch);

            if (createdSandwitch == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to create sandwich.");
                return Page();
            }

            return RedirectToPage("SwList"); // Redirect to list page after creation
        }
    }
}