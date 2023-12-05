using FA.JustBlog.Model;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Core.Areas.User.Controllers
{
    [Area("User")]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Employee obj)
        {
            if (obj.StartDate > obj.EndDate)
            {
                ModelState.AddModelError("EndDate", "End Date must be larger than Start Date!");
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
