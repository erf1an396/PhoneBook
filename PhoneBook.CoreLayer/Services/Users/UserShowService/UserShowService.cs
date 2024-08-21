using Microsoft.EntityFrameworkCore;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.Services.Users.UserShowService
{
    public class UserShowService: IUserShowService
    {
        private readonly AppDbContext _appDbContext;

        public UserShowService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task UpdateUserAsync(UserEditDto userEditDto)
        {
            var user = await _appDbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == userEditDto.Id);

            if (user == null) return;

            user.UserName = userEditDto.UserName;
            user.FullName = userEditDto.FullName;

            user.UserRoles.Clear();


            foreach (var roleId in userEditDto.RoleIds)
            {
                var role = await _appDbContext.Roles.FindAsync(roleId);
                if (role != null)
                {
                    user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
                }
            }

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();

            //var roles = await _appDbContext.Roles
            //    .Where(r => userEditDto.RoleIds.Contains(r.Id))
            //    .ToListAsync();

            //foreach (var role in roles)
            //{
            //    user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            //}

            //await _appDbContext.SaveChangesAsync();

        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);

                await _appDbContext.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {


            var users = await _appDbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            FullName = u.FullName,
            CreatedDate = u.CreatedDate,
            Role = u.UserRoles.Select(ur => ur.Role.Name).ToList()
            })
        .ToListAsync();

            return users;

        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _appDbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                CreatedDate = user.CreatedDate,
                Role = user.UserRoles.Select(ur => ur.Role.Name).ToList() 
            };
        }
    }
}
