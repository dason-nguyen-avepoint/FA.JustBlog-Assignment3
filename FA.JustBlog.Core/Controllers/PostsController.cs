using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;

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
    }
}
