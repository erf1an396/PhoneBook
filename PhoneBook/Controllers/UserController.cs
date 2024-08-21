using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.CoreLayer.Services.Users;
using PhoneBook.CoreLayer.Services.Users.UserShowService;

namespace PhoneBook.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserShowService _userShowService;

        public UserController(IUserShowService userShowService)
        {
            _userShowService = userShowService;
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
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userShowService.GetUserByIdAsync(id);
            return Json(user);
        }

        [HttpPost]
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid user data.");

            await _userShowService.DeleteUserAsync(id);
            return Ok();
        }


    }
}
