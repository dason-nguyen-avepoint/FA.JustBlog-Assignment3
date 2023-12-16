using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace FA.JustBlog.Core.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Posts> listPosts = _db.Posts.Include(x=> x.Categories).Where(x => x.isPublised && !String.IsNullOrEmpty(x.Categories.Name)).OrderByDescending(x => x.CreatedDate).ToList();
            return View(listPosts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult MostViewedPosts()
        {
            return View();
        }
        public IActionResult LatestPosts()
        {
            return View();
        }
        // GET: Home
        public IActionResult Capcha()
        {
            return View(new RECaptcha());
        }

        [HttpPost]
        public JsonResult AjaxMethod(string response)
        {
            RECaptcha recaptcha = new RECaptcha();
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + recaptcha.Secret + "&response=" + response;
            recaptcha.Response = (new WebClient()).DownloadString(url);
            return Json(recaptcha);
        }
    }
}
