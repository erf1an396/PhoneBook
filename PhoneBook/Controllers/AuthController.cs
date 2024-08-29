
using Microsoft.AspNetCore.Mvc;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.CoreLayer.Services.Users;
using PhoneBook.Models;
using PhoneBook.CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using PhoneBook.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;


namespace PhoneBook.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public AuthController(IUserService userService , SignInManager<ApplicationUser> signInManager , UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel model )
        {
            

            if (!ModelState.IsValid) return View(model);

            

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);


            
            return RedirectToAction("Index", "Home");
            
            
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _userService.RegisterUserAsync(new UserRegisterDto
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Password = model.Password
            });

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            //return View(model);
            else
            {
                return RedirectToAction("Login", "Auth");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

    }




}

