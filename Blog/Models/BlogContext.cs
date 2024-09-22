using Blog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new List<User>{
                new User()
                {
                    Id = 1,
                    Name = "Viktor",
                    Email = "abc@gmail.com",
                    Password = MyExt.HashPassword("123"),
                    Biography = "Hi! I'm Viktor"
                },
                new User()
                {
                    Id = 2,
                    Name = "Bohdan",
                    Email = "cba@gmail.com",
                    Password = MyExt.HashPassword("321"),
                    Biography = "Hi! I'm Bohdan"
                }
            });
            modelBuilder.Entity<Post>().HasData(new List<Post>{
                new Post() {
                    Id = 1,
                    Title = "The Science Behind Building Habits That Last",
                    Annotation = "Want to build new habits but keep failing? Discover the" +
                    "science-backed strategies that can help you create habits that stick for life.",
                    Content = "Forming new habits can feel like an uphill battle, but understanding " +
                    "the science behind habit formation can make the process smoother. According to " +
                    "research, habits are formed through a process called “habit loop,” which consists " +
                    "of a cue, routine, and reward.\r\n\r\nStart Small: Begin with tiny, manageable " +
                    "changes. Instead of aiming to run 5 miles every day, start with a 5-minute jog. " +
                    "This reduces the mental barrier to starting.\r\n\r\nConsistency Over Perfection: " +
                    "Aim for consistency. It's better to be consistent with a small habit than to aim " +
                    "for perfection and give up. Missing a day isn’t the end; just pick up where you " +
                    "left off.\r\n\r\nReward Yourself: A reward reinforces the behavior, making it " +
                    "more likely to stick. It could be as simple as a mental pat on the back, or " +
                    "treating yourself after a week of success.\r\n\r\nUnderstanding the psychology " +
                    "of habits can turn your resolutions into routines. Start small, stay consistent, " +
                    "and enjoy the rewards of lasting change.",
                    AuthorId = 1,
                    PublishDate = DateTime.Now,
                    Likes = 120,
                    Dislikes = 38
                },
                new Post() {
                    Id = 2,
                    Title = "The Future of Remote Work: Trends to Watch in 2024",
                    Annotation = "Remote work has changed the landscape of employment. Here's what you " +
                    "need to know about the emerging trends that will shape the future of remote work in " +
                    "2024 and beyond.",
                    Content = "Remote work is no longer just a temporary solution but a permanent " +
                    "shift in the way we approach work. As we move into 2024, several trends are " +
                    "emerging that will define the future of remote work:\r\n\r\nHybrid Work Models: " +
                    "Companies are increasingly adopting hybrid work models, allowing employees to " +
                    "split their time between home and office. This offers flexibility while " +
                    "maintaining some in-person collaboration.\r\n\r\nFocus on Mental Health: With " +
                    "remote work blurring the boundaries between work and life, mental health has " +
                    "become a priority. Companies are offering more wellness programs, mental health " +
                    "days, and resources to support employees.\r\n\r\nTech-Driven Collaboration: " +
                    "Advancements in technology, such as AI-driven tools and virtual reality, are " +
                    "making remote collaboration more effective and engaging. These tools aim to " +
                    "recreate the experience of working side-by-side, even when miles apart.\r\n\r\n" +
                    "Decentralized Workforces: The concept of hiring talent from anywhere is " +
                    "becoming mainstream. Companies are building decentralized teams, tapping into " +
                    "a global talent pool without the constraints of location.\r\n\r\nAs we continue " +
                    "to navigate the remote work landscape, staying ahead of these trends will be " +
                    "key to thriving in the new world of work.\r\n\r\n",
                    AuthorId = 1,
                    PublishDate = DateTime.Now,
                    Likes = 31,
                    Dislikes = 2
                },
                new Post() {
                    Id = 3,
                    Title = "Mindful Eating: A Path to Health and Happiness",
                    Annotation = "Discover the practice of mindful eating and how it can transform " +
                    "your relationship with food, leading to better health and a more fulfilling life.",
                    Content = "In a world full of distractions, eating has often become a mindless " +
                    "activity. Mindful eating is about bringing full attention to the experience of " +
                    "eating, and it can have profound effects on your health and well-being.\r\n\r\n" +
                    "Slow Down: Eating slowly allows you to savor each bite and recognize when you’re " +
                    "full. This not only improves digestion but also prevents overeating.\r\n\r\n" +
                    "Listen to Your Body: Pay attention to hunger and fullness cues. Eat when you’re " +
                    "hungry, and stop when you’re satisfied, not stuffed. This approach respects your " +
                    "body’s natural signals.\r\n\r\nEngage All Your Senses: Notice the colors, " +
                    "textures, and aromas of your food. Engaging your senses enhances the eating " +
                    "experience and makes meals more enjoyable.\r\n\r\nEliminate Distractions: Turn " +
                    "off the TV, put away your phone, and focus solely on your meal. This helps you " +
                    "fully appreciate your food and be more aware of how much you're eating.\r\n\r\n" +
                    "Mindful eating is not about strict rules or diets but about fostering a healthy, " +
                    "positive relationship with food. By eating with intention and awareness, you " +
                    "can enjoy your meals more and improve your overall well-being.",
                    AuthorId = 2,
                    PublishDate = DateTime.Now,
                    Likes = 568,
                    Dislikes = 99
                }
            });
            modelBuilder.Entity<Comment>().HasData(new List<Comment>{
                new Comment()
                {
                    Id = 1,
                    AuthorId = 1,
                    PostId = 1,
                    PublishDate = DateTime.Now,
                    Text = "Good news!"
                },
                new Comment()
                {
                    Id = 2,
                    AuthorId = 1,
                    PostId = 2,
                    PublishDate = DateTime.Now,
                    Text = "I don't really understand the meaning."
                },
                new Comment()
                {
                    Id = 3,
                    AuthorId = 1,
                    PostId = 1,
                    PublishDate = DateTime.Now,
                    Text = "I appreciate that ;)"
                }
            });
        }
    }
}
