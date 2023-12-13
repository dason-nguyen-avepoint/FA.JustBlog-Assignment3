using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FA.JustBlog.Core.Paging;

namespace FA.JustBlog.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdmin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index(string?sortBy, string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize = 3)
        {
            var _user = await _userManager.GetUserAsync(User);
            ViewBag.Role = await _userManager.GetRolesAsync(_user);
            var posts = from x in _context.Posts select x;
            var categories = from y in _context.Categories select y;
            var interests = from z in _context.InterestPosts select z;
            var groupInterest = _context.InterestPosts
                .GroupBy(x => x.PostId)
                .Select(x => new { postId = x.Key, rate = x.Average(p => p.Rate) });
            //var listPost = from post in posts
            //                      join category in categories on post.CategoryId equals category.CategoryId
            //                      join interest in groupInterest on post.Id equals interest.postId
            //                      select (new Posts
            //                      {
            //                          Id = post.Id,
            //                          Title = post.Title,
            //                          Description = post.Description,
            //                          CreatedDate = post.CreatedDate,
            //                          ViewCount = post.ViewCount,
            //                          CategoryId = post.CategoryId,
            //                          CateName = category.Name,
            //                          Rate = (int)interest.rate,
            //                          isPublised = post.isPublised
            //                      });
            var listPost = from post in posts
                           join category in categories on post.CategoryId equals category.CategoryId into temp
                           from c in temp.DefaultIfEmpty()
                           join interest in groupInterest on post.Id equals interest.postId
                           select new Posts
                           {
                               Id = post.Id,
                               Title = post.Title,
                               Description = post.Description,
                               CreatedDate = post.CreatedDate,
                               ViewCount = post.ViewCount,
                               CategoryId = post.CategoryId,
                               CateName = c == null ? null : c.Name,
                               Rate = (int)interest.rate,
                               isPublised = post.isPublised
                           };
            switch (sortBy)
            {
                case "Latest Posts":
                    listPost = listPost.Where(x => x.CreatedDate.Date == DateTime.Now.Date);
                    break;
                case "Most Viewed":
                    listPost = listPost.OrderByDescending(x => x.ViewCount);
                    break;
                case "Interesting":
                    listPost = listPost.OrderByDescending(x => x.Rate);
                    break;
                case "Published":
                    listPost = listPost.Where(x => x.isPublised);
                    break;
                case "Un-published":
                    listPost = listPost.Where(x => !x.isPublised);
                    break;
                default:
                    break;
            }
            ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["dateSort"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["viewSort"] = String.IsNullOrEmpty(sortOrder) ? "view_desc" : "";
            ViewData["nameCateSort"] = String.IsNullOrEmpty(sortOrder) ? "nameCate_desc" : "";
            ViewData["rateSort"] = String.IsNullOrEmpty(sortOrder) ? "rate_desc" : "";
            ViewData["publishSort"] = String.IsNullOrEmpty(sortOrder) ? "publish_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                listPost = listPost.Where(s => s.Title.Contains(searchString)
                                    || s.CateName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    listPost = listPost.OrderByDescending(x => x.Title);
                    break;
                case "date_desc":
                    listPost = listPost.OrderByDescending(x => x.CreatedDate);
                    break;
                case "view_desc":
                    listPost = listPost.OrderByDescending(x => x.ViewCount);
                    break;
                case "nameCate_desc":
                    listPost = listPost.OrderByDescending(x => x.CateName);
                    break;
                case "rate_desc":
                    listPost = listPost.OrderByDescending(x => x.Rate);
                    break;
                case "publish_desc":
                    listPost = listPost.OrderByDescending(x => x.isPublised);
                    break;
                default:
                    break;

            }
            ViewBag.PageSize = pageSize;
            return View(await PaginatedList<Posts>.CreateAsync(listPost.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        [Authorize(Policy = "RequireAdminContri")]
        public async Task<IActionResult> Create()
        {
            var _user = await _userManager.GetUserAsync(User);
            ViewBag.Role = await _userManager.GetRolesAsync(_user);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewBag.TagName = _context.Tags.ToList();
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminContri")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedDate,Content,ViewCount,CategoryId, isPublised")] Posts posts, List<int>Tags)
        {
            var _user = await _userManager.GetUserAsync(User);
            ViewBag.Role = await _userManager.GetRolesAsync(_user);
            if (ModelState.IsValid)
            {
                _context.Add(posts);
                await _context.SaveChangesAsync();
                foreach (var item in Tags)
                {
                    _context.TagsPost.Add(new TagPost
                    {
                        PostId = posts.Id,
                        TagId = item
                    });
                }
                //Thieu nghiep vu rate
                var interestPost = new InterestPost()
                {
                    PostId = posts.Id,
                    Rate = 0
                };
                _context.InterestPosts.Add(interestPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", posts.CategoryId);
            ViewBag.TagName = _context.Tags.ToList();
            return View(posts);
        }

        // GET: Admin/Posts/Edit/5
        [Authorize(Policy = "RequireAdminContri")]
        public async Task<IActionResult> Edit(int? id)
        {
            var _user = await _userManager.GetUserAsync(User);
            ViewBag.Role = await _userManager.GetRolesAsync(_user);

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
            //select* from Tags as T
            //left join
            //(select* from TagsPost where TagsPost.PostId = 3) as TP on T.TagId = TP.TagId;
            ViewBag.CheckTagState = from t in _context.Tags.ToList()
                        join tp in _context.TagsPost.Where(tp => tp.PostId == id).ToList()
                        on t.TagId equals tp.TagId
                        into joinedPost
                        from subTp in joinedPost.DefaultIfEmpty()
                        select new
                        {
                            TagId = t.TagId,
                            Name = t.Name,
                            PostId = subTp?.PostId ?? null
                        };

            return View(posts);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminContri")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreatedDate,Content,ViewCount,CategoryId,isPublised")] Posts posts, List<int> Tags)
        {
            if (id != posts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldPostTag = _context.TagsPost.Where(x => x.PostId == posts.Id).ToList();
                    _context.TagsPost.RemoveRange(oldPostTag);
                    foreach (var item in Tags)
                    {
                        _context.TagsPost.Add(new TagPost
                        {
                            PostId = posts.Id,
                            TagId = item
                        });
                    }
                    _context.Posts.Update(posts);
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
            ViewBag.CheckTagState = from t in _context.Tags.ToList()
                                    join tp in _context.TagsPost.Where(tp => tp.PostId == id).ToList()
                                    on t.TagId equals tp.TagId
                                    into joinedPost
                                    from subTp in joinedPost.DefaultIfEmpty()
                                    select new
                                    {
                                        TagId = t.TagId,
                                        Name = t.Name,
                                        PostId = subTp?.PostId ?? null
                                    };
            return View(posts);
        }

        // GET: Admin/Posts/Delete/5
        [Authorize(Roles ="Admin")]
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
        [Authorize(Roles = "Admin")]
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
        //[HttpGet]
        //public IActionResult GetPosts(string? sortBy)
        //{
        //    IEnumerable<Posts> listPost = null;
        //    if ("Latest Posts".Equals(sortBy))
        //    {
        //        listPost = _context.Posts.Include(x => x.Categories).Where(x => x.CreatedDate.Date == DateTime.Now.Date).ToList();
        //        ViewBag.SortBy = "Lastest Posts";
        //    }
        //    else if ("Most Viewed".Equals(sortBy))
        //    {
        //        listPost = _context.Posts.Include(x => x.Categories).OrderByDescending(x => x.ViewCount).ToList();
        //        ViewBag.SortBy = "The Most Viewed Post";
        //    }
        //    else if ("Interesting".Equals(sortBy))
        //    {
        //        var posts = _context.Posts.ToList();
        //        var categories = _context.Categories.ToList();
        //        var interests = _context.InterestPosts.ToList();
        //        var groupInterest = _context.InterestPosts
        //            .GroupBy(x => x.PostId)
        //            .Select(x => new { postId = x.Key, rate = x.Average(p => p.Rate) });
        //        var InterestingRate = from post in posts
        //                              join category in categories on post.CategoryId equals category.CategoryId
        //                              join interest in groupInterest on post.Id equals interest.postId
        //                              select (new
        //                              {
        //                                  post.Id,
        //                                  post.Title,
        //                                  post.Description,
        //                                  post.CreatedDate,
        //                                  post.ViewCount,
        //                                  post.CategoryId,
        //                                  category.Name,
        //                                  rate = (int)interest.rate
        //                              });
        //        ViewBag.SortBy = "List Interest Rate Post";
        //        return Json(new { data = InterestingRate });
        //    }
        //    else if ("Published".Equals(sortBy))
        //    {
        //        listPost = _context.Posts.Include(x => x.Categories).Where(x => x.isPublised).ToList();
        //        ViewBag.SortBy = "List Published Post";
        //    }
        //    else if ("Un-published".Equals(sortBy))
        //    {
        //        listPost = _context.Posts.Include(x => x.Categories).Where(x => !x.isPublised).ToList();
        //        ViewBag.SortBy = "List Unpublished Post";
        //    }
        //    else
        //    {
        //        ViewBag.SortBy = "List Post";
        //        listPost = _context.Posts.Include(x => x.Categories).ToList();
        //    }
        //    return Json(new { data = listPost });
        //}
    }
}
