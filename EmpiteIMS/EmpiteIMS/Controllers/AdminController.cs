using EmpiteIMS.Models.Domain;
using EmpiteIMS.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmpiteIMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> ShowUserList()
        {
            var users = userManager.Users;
            return View(users);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("Admin/ViewUser/{Id:int}")]
        public async Task<IActionResult> ViewUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                await userManager.GetClaimsAsync(user);
            }
            return RedirectToAction("GetUsers");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ViewUser(UserListModel model)
        {
            var user = userManager.FindByIdAsync(model.Id).GetAwaiter().GetResult();
            ApplicationUser selectedtUser = user;

            if (user != null)
            {
                user.UserName = model.Username;
                user.Email = model.Email;
                var oldRole = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(selectedtUser, oldRole);
                await userManager.AddToRoleAsync(selectedtUser, model.Role);

                user.LockoutEnabled = model.UserStatus;

                await userManager.UpdateAsync(user);

                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("GetUsers");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(UserListModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user != null)
            {
                userManager.DeleteAsync(user);

                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("GetUsers");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GoBack()
        {
            return RedirectToAction("GetUsers");
        }
    }
}
