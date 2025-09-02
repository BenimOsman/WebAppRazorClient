using System.ComponentModel.DataAnnotations;

namespace WebAppRazorSandwitchClient
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }                                                                   // Email Address: Required

        [Required]
        [DataType(DataType.Password)]                                                                       // Specifies that Password field should be Hidden
        public string Password { get; set; }                                                                // Password: Required
    }
}

