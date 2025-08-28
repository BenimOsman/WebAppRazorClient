using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazorClient;


namespace WebAppRazorClient.Pages
{
    public class SwDetailsModel : PageModel
    {
        private readonly SandwichService _service;

        public SwDetailsModel(SandwichService service)
        {
            _service = service;
        }

        public SandwichModel SandwitchDetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var sandwich = await _service.GetSandwitchByIdAsync(id);
            if (sandwich == null)
            {
                return NotFound();
            }

            SandwitchDetails = sandwich;
            return Page();
        }
    }
}