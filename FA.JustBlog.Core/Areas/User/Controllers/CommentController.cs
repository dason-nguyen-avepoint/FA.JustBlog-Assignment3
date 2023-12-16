using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Areas.User.Controllers
{
    [Area("User")]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> GetCommentsOfPost(int postId)
        {
            var listComments = await _context.Comments.Where(c => c.postId == postId).ToListAsync();
            return PartialView("_CommentsOfPost", listComments);
        }
        [HttpPost]

        public IActionResult CreateCommentInPost(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var userName = _userManager.GetUserName(User);
                comment.Title = userName + "comment";
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction(nameof(GetCommentsOfPost), new { postId = comment.postId });
            }
            else
            {
                return RedirectToAction(nameof(GetCommentsOfPost), new { postId = comment.postId });
            }

        }
    }
}
