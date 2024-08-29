using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.CoreLayer.Services.Users;
using PhoneBook.CoreLayer.Services.Users.UserShowService;
using PhoneBook.DataLayer.Entities;

namespace PhoneBook.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserShowService _userShowService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserShowService userShowService , UserManager<ApplicationUser> userManager)
        {
            _userShowService = userShowService;
            _userManager = userManager;
        }


        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userShowService.GetAllUsersAsync();
            return Json(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userShowService.GetUserByIdAsync(id);
            return Json(user);
        }

        [HttpPost]
        [Authorize(Roles = "UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserEditDto userEditDto)
        {
            if(ModelState.IsValid)
            {
                await _userShowService.UpdateUserAsync(userEditDto);
                return Ok();
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPost]
        [Authorize(Roles = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid user data.");

            await _userShowService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            // Return the roles as a JSON array
            return Json(roles);
        }



    }
}
