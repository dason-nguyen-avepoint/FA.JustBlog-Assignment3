using FA.JustBlog.Model;
using FA.JustBlog.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FA.JustBlog.Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _user = await _userManager.GetUserAsync(User);
            ViewBag.Role = await _userManager.GetRolesAsync(_user);
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
                               user.EmailConfirmed
                           });
            return View(listUser);
        }

        public IActionResult Create()
        {
            ViewData["ListRole"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel model, string RoleId)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.FullName,
                    Address = model.Address,
                    Age = model.Age
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(RoleId);
                    await _userManager.AddToRoleAsync(user, role.Name);

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = "" },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            ViewData["ListRole"] = new SelectList(_context.Roles, "Id", "Name");
            return View(model);
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
                if (!String.IsNullOrEmpty(RoleId))
                {
                    //Get role
                    var role = await _userManager.GetRolesAsync(_user);
                    await _userManager.RemoveFromRolesAsync(_user, role);
                    var newRole = await _roleManager.FindByIdAsync(RoleId);
                    await _userManager.AddToRoleAsync(_user, newRole.Name);
                }
                _user.Name = user.Name;
                _user.Address = user.Address;
                _user.Age = user.Age;
                _user.AboutMe = user.AboutMe;
                _user.PhoneNumber = user.PhoneNumber;

                _context.ApplicationUsers.Update(_user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Role = new SelectList(_context.Roles.ToList());
            return View(user);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var _user = await _userManager.FindByIdAsync(id);

            if (_user == null)
            {
                return NotFound();
            }
            ViewBag.RoleName = await _userManager.GetRolesAsync(_user);
            return View(_user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var _user = await _userManager.FindByIdAsync(id);
            if (_user != null)
            {
                await _userManager.DeleteAsync(_user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
