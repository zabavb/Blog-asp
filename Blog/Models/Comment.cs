using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }

        [NotMapped]
        public User Author { get; set; } = new User();
        public int PostId { get; set; }

        [NotMapped]
        public Post Post { get; set; } = new Post();
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public string? Text { get; set; }
    }
}