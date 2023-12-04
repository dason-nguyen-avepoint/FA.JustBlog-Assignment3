using FA.JustBlog.DataAccess;
using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Core.Views.Shared.Components.CategoryTag
{
    public class CategoryTag :ViewComponent
    {
        private ApplicationDbContext _db;
        public CategoryTag(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            IEnumerable<Category> obj = _db.Categories.ToList();
            return View("CategoryTag",obj);
        }
    }
}
