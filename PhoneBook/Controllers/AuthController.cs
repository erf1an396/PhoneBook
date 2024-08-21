
using Microsoft.AspNetCore.Mvc;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.CoreLayer.Services.Users;
using PhoneBook.Models;
using PhoneBook.CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using PhoneBook.DataLayer.Entities;

namespace PhoneBook.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            var User = _userService.LoginUser(new UserLoginDto()
            {
                UserName = model.UserName,
                Password = model.Password
            });

            if(User == null)
            {
                ModelState.AddModelError("UserName", "No information found");
                return View(model);
            }

            #region Claim
            List<Claim> claims = new List<Claim>() {
                new Claim("Test","Test"),
                new Claim(ClaimTypes.NameIdentifier , User.Id.ToString()),
                new Claim(ClaimTypes.Name , User.UserName),
                //new Claim(ClaimTypes.Role , "admin")

            };
            foreach (var role in User.Role)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var ClaimPrinciple = new ClaimsPrincipal(Identity);
            var Properties = new AuthenticationProperties()
            {
                IsPersistent = true,
            };
            HttpContext.SignInAsync(ClaimPrinciple, Properties);
            return RedirectToAction("Index", "Home");

            #endregion

        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            var result = _userService.RegisterUser(new UserRegisterDto()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Password = model.Password
            });



            var resultStatus = await result;
            if (resultStatus.Status == OperationResultStatus.NotFound)
            {
                ModelState.AddModelError("UserName", "No information found");
                return View(model);
            }



            return RedirectToAction("Login", "Auth");

            


        }


    
}
}
