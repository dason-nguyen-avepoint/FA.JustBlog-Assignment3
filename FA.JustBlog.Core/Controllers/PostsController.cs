using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Posts> listPosts = _db.Posts.OrderByDescending(x => x.CreatedDate).ToList();
            return View(listPosts);
        }
        [Route("post/{year:int}/{month:int}/{title}")]
        public IActionResult Details(int year, int month, [FromRoute] string? title)
        {
            // Decode the URL-encoded title
            title = Uri.UnescapeDataString(title);
            // Replace hyphens with spaces
            title = title.Replace("-", " ");
            var obj = _db.Posts.Include(x => x.Categories).FirstOrDefault(x => x.CreatedDate.Year == year && x.CreatedDate.Month == month && x.Title.ToLower() == title.ToLower());
            if (obj == null)
            {
                return NotFound();
            }
            obj.ViewCount += 1;
            _db.SaveChanges();
            return View(obj);
        }
    }
}
