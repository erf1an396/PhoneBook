using Microsoft.EntityFrameworkCore;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhoneBook.CoreLayer.Services.Users.UserShowService
{
    public class UserShowService: IUserShowService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole>  _roleManager;

        public UserShowService(UserManager<ApplicationUser> userManager , RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task UpdateUserAsync(UserEditDto userEditDto)
        {
            var user = await _userManager.FindByIdAsync(userEditDto.Id);
            if (user == null)
                return;

            user.UserName = userEditDto.UserName;
            user.FullName = userEditDto.FullName;

            
            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            foreach (var roleItem in userEditDto.RoleIds)
            {
               var role = await _roleManager.FindByIdAsync(roleItem);
               if(role == null) continue;
               var t = await _userManager.AddToRoleAsync(user, role.Name);
            }
            

            //var roles = await _appDbContext.Roles
            //    .Where(r => userEditDto.RoleIds.Contains(r.Id))
            //    .ToListAsync();

            //foreach (var role in roles)
            //{
            //    user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            //}

            //await _appDbContext.SaveChangesAsync();

        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {


            var users = await _userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new UserDto
                {
                    Id = u.Id.ToString(),
                    UserName = u.UserName,
                    FullName = u.FullName,
                    CreatedDate = u.CreatedDate,
                    Role = u.UserRoles.Select(ur => ur.Role.Name).ToList()
                })
                .ToListAsync();

            return users;

        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));

            if (user == null) return null;

            return new UserDto
            {
                Id =user.Id.ToString() ,
                UserName = user.UserName,
                FullName = user.FullName,
                CreatedDate = user.CreatedDate,
                Role = user.UserRoles.Select(ur => ur.Role.Name).ToList()
            };
        }
    }
}
