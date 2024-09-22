using Blog.Models;
using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private BlogContext Context { get; set; }

        public PostController(BlogContext context)
        {
            Context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View(new PostModel());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostModel model)
        {
            if (ModelState.IsValid)
            {
                string? email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (email == null)
                    return Unauthorized();

                User? author = await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (author == null)
                    return NotFound();

                Post post = new Post()
                {
                    Title = model.Title,
                    Annotation = model.Annotation,
                    Content = model.Content,
                    Author = author,
                    AuthorId = author.Id
                };

                await Context.Posts.AddAsync(post);
                await Context.SaveChangesAsync();

                Post? tmpPost = await Context.Posts.FirstOrDefaultAsync(p => p.Title == post.Title && p.PublishDate == post.PublishDate && p.AuthorId == post.AuthorId);
                if (tmpPost == null)
                    return NotFound();

                return RedirectToAction("One", new { id = tmpPost.Id });
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetLike(int id)
        {
            Post? post = await Context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
                return NotFound();

            post.Likes++;
            Context.Posts.Update(post);
            await Context.SaveChangesAsync();

            return RedirectToAction("One", new { id });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetDislike(int id)
        {
            Post? post = await Context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
                return NotFound();

            post.Dislikes++;
            Context.Posts.Update(post);
            await Context.SaveChangesAsync();

            return RedirectToAction("One", new { id });
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateComment(int id)
        {
            return View(new CommentModel() { PostId = id });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentModel model, int postId)
        {
            if (ModelState.IsValid)
            {
                string? email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (email == null)
                    return Unauthorized();

                User? author = await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
                Post? post = await Context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                if (author == null || post == null)
                    return NotFound();

                Comment comment = new Comment()
                {
                    AuthorId = author.Id,
                    Author = author,
                    PostId = post.Id,
                    Post = post,
                    Text = model.Text
                };

                await Context.Comments.AddAsync(comment);
                await Context.SaveChangesAsync();

                return RedirectToAction("One", "Post", new { id = postId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Many()
        {
            List<Post> posts = await Context.Posts.ToListAsync();
            return View(posts);
        }
        [HttpPost]
        public IActionResult Many(int id)
        {
            return RedirectToAction("One", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> One(int id)
        {
            Post? post = await Context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
                return NotFound();

            User? author = await Context.Users.FirstOrDefaultAsync(u => u.Id == post.AuthorId);
            if (author == null)
                return NotFound();
            post.Author = author;

            List<Comment> comments = await Context.Comments.Where(c => c.PostId == id).ToListAsync();
            foreach (Comment comment in comments)
            {
                User? user = await Context.Users.FirstOrDefaultAsync(u => u.Id == comment.AuthorId);
                comment.Author = user != null ? user : new User();
            }
            post.Comments = comments;

            return View(post);
        }
        [HttpPost]
        public IActionResult One(int id, int _)
        {
            return RedirectToAction("One", "User", new { id });
        }
    }
}
