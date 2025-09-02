using System.ComponentModel.DataAnnotations;

namespace WebAppRazorSandwitchClient
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }                                                          // Email Address: Required

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }                                                       // Password: Required

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]                             
        public string? ConfirmPassword { get; set; }                                                // Confirm Password: Compare with Password
    }
}