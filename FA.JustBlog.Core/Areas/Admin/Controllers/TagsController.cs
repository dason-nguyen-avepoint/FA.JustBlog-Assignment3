using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using FA.JustBlog.Utils;
using Microsoft.AspNetCore.Authorization;
using FA.JustBlog.Core.Paging;
using Microsoft.AspNetCore.Identity;

namespace FA.JustBlog.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdmin")]
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TagsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Admin/Tags
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, int pageSize = 3)
        {
            var _user = await _userManager.GetUserAsync(User);
            ViewBag.Role = await _userManager.GetRolesAsync(_user);

            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var tagList = from x in _context.Tags select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                tagList = tagList.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    tagList = tagList.OrderByDescending(x => x.Name);
                    break;
                default:
                    tagList = tagList.OrderBy(x => x.Name);
                    break;

            }
            ViewBag.PageSize = pageSize;
            return View(await PaginatedList<Tag>.CreateAsync(tagList.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Admin/Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [Authorize(Policy = "RequireAdminContri")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdminContri")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,Name")] Tag tag)
        {
            if (_context.Tags.FirstOrDefault(x => x.Name == tag.Name) != null)
            {
                ModelState.AddModelError("name", "This Tag Name has been existed! Please try another name!");
            }
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        [Authorize(Policy = "RequireAdminContri")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        [Authorize(Policy = "RequireAdminContri")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagId,Name")] Tag tag)
        {
            if (id != tag.TagId)
            {
                return NotFound();
            }
            if (_context.Tags.FirstOrDefault(x => x.Name == tag.Name && x.TagId != tag.TagId) != null)
            {
                ModelState.AddModelError("name", "This Tag Name has been existed! Please try another name!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.TagId))
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
            return View(tag);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.TagId == id);
        }
    }
}
