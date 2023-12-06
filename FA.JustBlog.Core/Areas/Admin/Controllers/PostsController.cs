using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using System.Globalization;

namespace FA.JustBlog.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedDate,Content,ViewCount,CategoryId, isPublised")] Posts posts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", posts.CategoryId);
            return View(posts);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", posts.CategoryId);
            return View(posts);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreatedDate,Content,ViewCount,CategoryId,isPublised")] Posts posts)
        {
            if (id != posts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", posts.CategoryId);
            return View(posts);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posts = await _context.Posts.FindAsync(id);
            if (posts != null)
            {
                _context.Posts.Remove(posts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
        [HttpGet]
        public IActionResult GetPosts(string? sortBy)
        {
            IEnumerable<Posts> listPost = null;
            if ("Latest Posts".Equals(sortBy))
            {
                listPost = _context.Posts.Include(x => x.Categories).Where(x => x.CreatedDate.Date == DateTime.Now.Date).ToList();
                ViewBag.SortBy = "Lastest Posts";
            }
            else if ("Most Viewed".Equals(sortBy))
            {
                listPost = _context.Posts.Include(x => x.Categories).OrderByDescending(x => x.ViewCount).ToList();
                ViewBag.SortBy = "The Most Viewed Post";
            }
            else if ("Interesting".Equals(sortBy))
            {
                var posts = _context.Posts.ToList();
                var categories = _context.Categories.ToList();
                var interests = _context.InterestPosts.ToList();
                var groupInterest = _context.InterestPosts
                    .GroupBy(x => x.PostId)
                    .Select(x => new { postId = x.Key, rate = x.Average(p => p.Rate) });
                var InterestingRate = from post in posts
                                      join category in categories on post.CategoryId equals category.CategoryId
                                      join interest in groupInterest on post.Id equals interest.postId
                                      select (new
                                      {
                                          post.Id,
                                          post.Title,
                                          post.Description,
                                          post.CreatedDate,
                                          post.ViewCount,
                                          post.CategoryId,
                                          category.Name,
                                          rate = (int)interest.rate
                                      });
                ViewBag.SortBy = "List Interest Rate Post";
                return View("Interesting", InterestingRate);
            }
            else if ("Published".Equals(sortBy))
            {
                listPost = _context.Posts.Include(x => x.Categories).Where(x => x.isPublised).ToList();
                ViewBag.SortBy = "List Published Post";
            }
            else if ("Un-published".Equals(sortBy))
            {
                listPost = _context.Posts.Include(x => x.Categories).Where(x => !x.isPublised).ToList();
                ViewBag.SortBy = "List Unpublished Post";
            }
            else
            {
                ViewBag.SortBy = "List Post";
                listPost = _context.Posts.Include(x => x.Categories).ToList();
            }
            return Json(new { data = listPost });
        }
    }
}
