using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class CommentModel
    {
        public int PostId { get; set; }

        [Required(ErrorMessage = "Text is required")]
        [Length(2, 256, ErrorMessage = "Minimum length 2, maximum length 256")]
        public string? Text { get; set; }
    }
}
