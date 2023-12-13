using FA.JustBlog.Core.Paging;
using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdmin")]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize = 3)
        {
            var _user = await _userManager.GetUserAsync(User);
            ViewBag.Role = await _userManager.GetRolesAsync(_user);

            ViewData["titleSort"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["contentSort"] = String.IsNullOrEmpty(sortOrder) ? "content_desc" : "";
            ViewData["userSort"] = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
            ViewData["postSort"] = String.IsNullOrEmpty(sortOrder) ? "post_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var comments = from x in _context.Comments select x;
            var users = from x in _userManager.Users select x;
            var posts = from x in _context.Posts select x;
            var listComment = from c in comments
                              join u in users on c.userId equals u.Id
                              join p in posts on c.postId equals p.Id
                              select (new Comment
                              {
                                  Id = c.Id,
                                  Title = c.Title,
                                  Content = c.Content,
                                  UserName = u.UserName,
                                  PostTitle = p.Title
                              });
            if (!String.IsNullOrEmpty(searchString))
            {
                listComment = listComment.Where(s => s.Title.Contains(searchString)
                                            || s.Content.Contains(searchString)
                                            || s.UserName.Contains(searchString)
                                            || s.PostTitle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    listComment = listComment.OrderByDescending(x => x.Title);
                    break;
                case "content_desc":
                    listComment = listComment.OrderByDescending(x => x.Content);
                    break;
                case "user_desc":
                    listComment = listComment.OrderByDescending(x => x.UserName);
                    break;
                case "post_desc":
                    listComment = listComment.OrderByDescending(x => x.PostTitle);
                    break;
                default:
                    break;

            }
            ViewBag.PageSize = pageSize;
            return View(await PaginatedList<Comment>.CreateAsync(listComment.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [Authorize(Policy = "RequireAdminContri")]
        public IActionResult Create()
        {
            ViewBag.User =  _userManager.Users.ToList();
            ViewData["Post"] = new SelectList(_context.Posts, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminContri")]
        public async Task<IActionResult> Create([Bind("Id ,Title, Content, userId, postId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.Include(x => x.Users).Include(x => x.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
        [Authorize(Policy = "RequireAdminContri")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments
                        .Include(x => x.Post)
                        .Include(x => x.Users)
                        .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
        [Authorize(Policy = "RequireAdminContri")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Title, Content, userId, postId, Users, Post")] Comment comment)
        //public async Task<IActionResult> Edit(int id, [FromForm] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            var post = _context.Posts.FirstOrDefault(x => x.Id == comment.postId);
            var user = _userManager.Users.FirstOrDefault(x => x.Id == comment.userId);
            comment.Post = post;
            comment.Users = user;

            return View(comment);
        }
        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var comment = await _context.Comments.Include(x => x.Users).Include(x => x.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.Include(x => x.Users).Include(x => x.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
