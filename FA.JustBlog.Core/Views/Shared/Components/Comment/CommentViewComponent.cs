using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Core.Views.Shared.Components.CommentView
{
    public class CommentViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public CommentViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(List<Comment> Comments, int PostId)
        {
            var item = new
            {
                comment = Comments,
                PostId = PostId
            };
            return View(item);
        }
    }
}
