using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Views.Shared.Components.TagPost
{
    public class TagPost : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public TagPost(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke(Posts obj)
        {
            var tagNames = _db.Posts
                        .Where(post => post.Id == obj.Id)
                        .SelectMany(post => post.TagPosts.Select(tagPost => tagPost.Tags.Name))
                        .ToList();
            return View("TagPost", tagNames);
        }
    }
}
