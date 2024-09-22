using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Length(2, 32, ErrorMessage = "Minimum length 2, maximum length 32")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Length(3, 32, ErrorMessage = "Password must be within range from 3 to 32")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confiramtion password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password didn't much")]
        [Display(Name = "Confirm password")]
        public string? ConfirmPassword { get; set; }

        [StringLength(512, ErrorMessage = "Maximum length 512 characters")]
        public string? Biography { get; set; }
    }
}
