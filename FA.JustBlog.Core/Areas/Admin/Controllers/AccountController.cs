using FA.JustBlog.Model;
using FA.JustBlog.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var listUser = from user in _context.ApplicationUsers.ToList()
                           join roleuser in _context.UserRoles.ToList() on user.Id equals roleuser.UserId
                           join role in _context.Roles.ToList() on roleuser.RoleId equals role.Id
                           select (new
                           {
                               user.Id,
                               user.Email,
                               FullName = user.Name,
                               user.Age,
                               user.PhoneNumber,
                               user.Address,
                               Role = role.Name,
                           });
            return View(listUser);
        }
        public IActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = _context.ApplicationUsers.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["Role"] = new SelectList(_context.Roles, "Id", "Name");
            //ViewBag.RoleId  = new SelectList(_context.Roles, "Id", "Name");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user, string? RoleId)
        {
            var _user = await _userManager.FindByIdAsync(user.Id);
            
            if (_user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if(!String.IsNullOrEmpty(RoleId))
                {
                    //Get role
                    var role = await _userManager.GetRolesAsync(_user);
                    await _userManager.RemoveFromRolesAsync(_user, role);
                    var newRole = await _roleManager.FindByIdAsync(RoleId);
                    await _userManager.AddToRoleAsync(_user, newRole.Name);
                }
                _user.Name = user.Name;
                _user.Email = user.Email;
                _user.Address = user.Address;
                _user.Age = user.Age;
                _user.AboutMe = user.AboutMe;
                _user.PhoneNumber = user.PhoneNumber;
                await _userManager.UpdateAsync(_user);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Role = new SelectList(_context.Roles.ToList());
            return View(user);
        }
    }
}
