using Microsoft.AspNetCore.Mvc;                 // ASP.NET Core MVC functionality
using Microsoft.AspNetCore.Mvc.RazorPages;      // Razor Pages support
using System.Collections.Generic;               // Collection types like List
using System.Threading.Tasks;                   // Async Task functionality

namespace WebAppRazorClient.Pages
{
    // PageModel for managing the sandwich list
    public class SwListModel : PageModel
    {
        private readonly SandwichService _service;                                                              // Service to interact with sandwich data

        public SwListModel(SandwichService service)                                                             // Constructor to inject SandwichService
        {
            _service = service;                                                                                 // Initialize service
        }

        // List property to hold sandwiches for display
        public List<SandwichModel> SwList { get; set; } = new();  // Initialize as empty list

        // OnGetAsync handles GET requests to load sandwich list
        public async Task OnGetAsync()
        {
            SwList = await _service.GetSandwiches();  // Fetch sandwiches from service
        }

        // OnPostDeleteAsync handles POST request to delete a sandwich
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteSandwitchAsync(id);  // Delete the sandwich by ID
            return RedirectToPage();  // Redirect to refresh the page
        }
    }
}