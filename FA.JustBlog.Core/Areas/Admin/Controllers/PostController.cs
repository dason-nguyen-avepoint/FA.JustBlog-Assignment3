using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FA.JustBlog.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PostController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string? sortBy)
        {
            IEnumerable<Posts> listPost = null;
            if ("Latest Posts".Equals(sortBy)) 
            {
                listPost = _db.Posts.Include(x => x.Categories).Where(x => x.CreatedDate == DateTime.Now).ToList();
            }else if("Most Viewed".Equals(sortBy))
            {
                listPost = _db.Posts.Include(x => x.Categories).OrderByDescending(x => x.ViewCount).ToList();
            }
            else if ("Interesting".Equals(sortBy))
            {
                var posts = _db.Posts.ToList();
                var categories = _db.Categories.ToList();
                var interests = _db.InterestPosts.ToList();
                //listPost = from post in posts
                //           join category in categories on post.CategoryId equals category.CategoryId
                //           join interest in interests on post.Id equals interest.PostId
                //           group interest by interest.PostId into postId
                //           select new
                //           {
                //               postId = postId.Key,
                //               Avg = postId.Average()
                //           };

                //select post.*, Sub.Rate, category.Name
                //from Posts as post
                //join Categories as category on post.CategoryId = category.CategoryId
                //join(select PostId, AVG(Rate) as Rate from InterestPosts as interest
                //                                      group by PostId) As Sub on post.Id = Sub.PostId
            }
            //else if ("Published".Equals(sortBy))
            //{
            //    listPost = _db.Posts.Include(x => x.Categories).Where(x => x.isPublished).ToList();
            //}
            //else if ("Un-published".Equals(sortBy))
            //{
            //    listPost = _db.Posts.Include(x => x.Categories).Where(x => !x.isPublished).ToList();
            //}
            else
            {
                listPost = _db.Posts.Include(x => x.Categories).ToList();
            }
            return View(listPost);
        }
    }
}
