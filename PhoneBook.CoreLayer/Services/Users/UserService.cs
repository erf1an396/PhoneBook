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

namespace PhoneBook.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

       

        public async Task <OperationResult> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var isUserNameExist = _appDbContext.Users.Any(u => u.UserName == userRegisterDto.UserName);
            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است ");


            var passwordHash = userRegisterDto.Password.EncodeToMd5();
            var user = new User
            {
                FullName = userRegisterDto.FullName,
                UserName = userRegisterDto.UserName,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                Password = passwordHash,
                UserRoles = new List<UserRole>()
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            var userRole = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == "user");
            if (userRole != null)
            {
                _appDbContext.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = userRole.Id });
                await _appDbContext.SaveChangesAsync();
            }


            return OperationResult.Success();
        }


        public UserDto LoginUser(UserLoginDto userLoginDto)
        {
            var PasswordHash = userLoginDto.Password.EncodeToMd5();
            var User = _appDbContext.Users
                 .Include(u => u.UserRoles) 
                  .ThenInclude(ur => ur.Role) 
                   .FirstOrDefault(u => u.UserName == userLoginDto.UserName && u.Password == PasswordHash);

            if (User == null)
                return null;

            var UserDto = new UserDto()
            {
                FullName = User.FullName,
                UserName = User.UserName,
                Password = User.Password,
                Role = User.UserRoles.Select(ur => ur.Role.Name).ToList(),
                CreatedDate = User.CreatedDate,
                Id = User.Id
            };
            return UserDto;


        }


    }

    
}
