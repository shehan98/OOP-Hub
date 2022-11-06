using EmpiteIMS.Models.DTO;
using EmpiteIMS.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpiteIMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserAuthenticationService _service;
        public LoginController(IUserAuthenticationService service)
        {
            this._service = service;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult UserCreation()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UserCreation(UserCreationModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View(model); 
            }
            var result = await _service.UserCreationAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(UserCreation));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("ShowInventory", "Inventory");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterAdmin()
        //{
        //    UserCreationModel model = new UserCreationModel
        //    {
        //        Username = "manager1",
        //        Email = "manager1@gmail.com",
        //        Password = "Manager1@12345#"
        //    };
        //    model.Role = "manager";
        //    var result = await _service.UserCreationAsync(model);
        //    return Ok(result);
        //}
    }
}
