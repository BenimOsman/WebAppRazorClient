using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazorClient;

namespace WebAppRazorClient.Pages
{
    public class SwEditModel : PageModel
    {
        private readonly SandwichService _service;

        public SwEditModel(SandwichService service)
        {
            _service = service;
        }

        [BindProperty]
        public SandwichModel EditSandwitch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var sandwich = await _service.GetSandwitchByIdAsync(id);
            if (sandwich == null)
            {
                return NotFound();
            }

            EditSandwitch = sandwich;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Call Update method that returns bool
            var success = await _service.UpdateSandwitchAsync(EditSandwitch);
            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Update failed.");
                return Page();
            }

            return RedirectToPage("SwList");
        }
    }
}
