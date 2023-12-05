using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Areas.User.Controllers
{
    [Area("User")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult EntityFramework()
        {
            IEnumerable<Posts> categoryPosts = _db.Posts.Include(x => x.Categories).Where(x => x.Categories.Name == "Entity Framework").ToList();
            return View(categoryPosts);
        }
        public IActionResult Mvc()
        {
            IEnumerable<Posts> categoryPosts = _db.Posts.Include(x => x.Categories).Where(x => x.Categories.Name == "MVC").ToList();
            return View(categoryPosts);
        }
    }
}
