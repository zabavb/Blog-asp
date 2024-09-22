using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class UserController : Controller
    {
        private BlogContext Context { get; set; }

        public UserController(BlogContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Many()
        {
            List<User> users = await Context.Users.ToListAsync();
            return View(users);
        }
        [HttpPost]
        public IActionResult Many(int id)
        {
            return RedirectToAction("One", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> One(int id)
        {
            User? user = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            return View(new
            {
                User = user,
                Posts = await Context.Posts.Where(p => p.AuthorId == id).ToListAsync(),
                Comments = await Context.Comments.Where(c => c.AuthorId == id).ToListAsync()
            });
        }
        [HttpPost]
        public IActionResult One(int id, int _)
        {
            return RedirectToAction("One", "Post", new { id });
        }
    }
}
