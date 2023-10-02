using DomainLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using ServiceLayer.DTOs;

namespace Web.UI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;
        public UserController(UserManager<AppUser> userManager, AppDbContext context, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> UserList()
        {

            //var users = _context.Users.Join(_context.UserRoles, user => user.Id, userRole => userRole.UserId, (user, userRole) => new
            //{
            //    user,
            //    userRole
            //}).Join(_context.Roles, two => two.userRole.RoleId, role => role.Id, (two, role) => new { two.user, two.userRole, role }).Where(x => x.role.Name != "SuperAdmin").Select(x => new AppUser
            //{
            //    Id = x.user.Id,
            //    UserName = x.user.UserName,
            //    Email = x.user.Email,
            //    AccessFailedCount = x.user.AccessFailedCount,
            //    PhoneNumber = x.user.PhoneNumber,
            //    PasswordHash = x.user.PasswordHash,
            //    LockoutEnabled = x.user.LockoutEnabled,
            //    LockoutEnd = x.user.LockoutEnd,
            //    NormalizedEmail = x.user.NormalizedEmail,
            //    Baskets = x.user.Baskets,
            //    Favorites = x.user.Favorites,
            //    EmailConfirmed = x.user.EmailConfirmed,
            //    ConcurrencyStamp = x.user.ConcurrencyStamp,
            //}).ToList();

            var filterusers = new List<AppUser>();

            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userroles = await _userManager.GetRolesAsync(user);
                if (!userroles.Contains("SuperAdmin"))
                    filterusers.Add(user);
            }

            return View(filterusers);
        }


        public async Task<IActionResult> AssignRole(int id)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            var model = new RoleAssignSendModel();
            var list = new List<RoleAssingListModel>();

            foreach (var role in roles)
            {
                list.Add(new()
                {
                    RoleId = role.Id,
                    Name = role.Name,
                    Exist = userRoles.Contains(role.Name)
                });
            }

            model.Roles = list;
            model.UserId = id;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleAssignSendModel model)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == model.UserId);
            var userroles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (role.Exist)
                {
                    if (!userroles.Contains(role.Name))
                        await _userManager.AddToRoleAsync(user, role.Name);
                }
                else
                {
                    if (userroles.Contains(role.Name))
                        await _userManager.RemoveFromRoleAsync(user, role.Name);

                }
            }

            return RedirectToAction("UserList");
        }
    }
}
