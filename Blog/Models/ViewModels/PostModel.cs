using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class PostModel
    {
        [Required(ErrorMessage = "Title is required")]
        [Length(2, 32, ErrorMessage = "Minimum length 2, maximum length 32")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Annotation is required")]
        [Length(2, 128, ErrorMessage = "Minimum length 2, maximum length 128")]
        public string? Annotation { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Length(64, 1024, ErrorMessage = "Minimum length 64, maximum length 1024")]
        public string? Content { get; set; }
    }
}
