using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Core.Views.Shared.Components.ProductBox
{

    public class ProductBox : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<Posts> obj)
        {
            return View(obj);
        }
    }
}
