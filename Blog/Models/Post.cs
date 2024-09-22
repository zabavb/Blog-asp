using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Annotation { get; set; }
        public string? Content { get; set; }

        public int AuthorId { get; set; }
        [NotMapped]
        public User Author { get; set; } = new User();

        public DateTime PublishDate { get; set; } = DateTime.Now;
        public double Likes { get; set; }
        public double Dislikes { get; set; }

        [NotMapped]
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
