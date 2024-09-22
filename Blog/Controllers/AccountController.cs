using Blog.Infrastructure;
using Blog.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Blog.Models.ViewModels;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private BlogContext Context { get; set; }

        public AccountController(BlogContext context) => Context = context;

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await Context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user == null)
                {
                    user = new User()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Password = MyExt.HashPassword(model.Password!),
                        Biography = model.Biography
                    };

                    Context.Users.Add(user);
                    await Context.SaveChangesAsync();
                    await Authenticate(user);

                    return RedirectToAction("MyProfile", new { user.Email });
                }
                else
                    ModelState.AddModelError("", "Such email already exists");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User? user = await Context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user == null || user.Password != MyExt.HashPassword(model.Password!))
                {
                    ModelState.AddModelError("", "Incorrect email or password");

                    return View(model);
                }

                await Authenticate(user);

                return RedirectToAction("MyProfile", new { user.Email });
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> MyProfile(string email)
        {
            User? user = await Context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound();

            return View(new
            {
                User = user,
                Posts = await Context.Posts.Where(p => p.AuthorId == user.Id).ToListAsync(),
                Comments = await Context.Comments.Where(c => c.AuthorId == user.Id).ToListAsync()
            });
        }

        public IActionResult Logout()
        {
            AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [NonAction]
        private async Task Authenticate(User user)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email!) };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
