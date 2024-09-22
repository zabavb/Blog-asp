using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Length(3, 32, ErrorMessage = "Password must be within range from 3 to 32")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
