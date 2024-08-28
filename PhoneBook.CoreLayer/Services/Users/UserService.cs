using PhoneBook.CoreLayer.Utilities;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PhoneBook.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {

        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager  = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            
        }



        public async Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var existingUser = await _userManager.FindByNameAsync(userRegisterDto.UserName);
            if (existingUser != null)
                return IdentityResult.Failed(new IdentityError { Description = "نام کاربری تکراری است" });

            var user = new ApplicationUser
            {
                FullName = userRegisterDto.FullName,
                UserName = userRegisterDto.UserName,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!result.Succeeded)
                return result;


            var userRole = await _roleManager.FindByNameAsync("user");
            if (userRole != null)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, userRole.Name);
                if (!roleResult.Succeeded)
                    return roleResult;
            }

            return IdentityResult.Success;
        }

        public async Task<UserDto> LoginUserAsync(UserLoginDto userLoginDto)
        {

            var result = await _signInManager.PasswordSignInAsync(userLoginDto.UserName, userLoginDto.Password, true, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

                return new UserDto
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    //Role = roles.ToList(),
                    CreatedDate = user.CreatedDate,
                    Id = user.Id
                };
            }

            return null;


        }


    }

    
}
