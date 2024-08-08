using PhoneBook.CoreLayer.Utilities;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

       

        public OperationResult RegisterUser(UserRegisterDto userRegisterDto)
        {
            var isUserNameExist = _appDbContext.Users.Any(u => u.UserName == userRegisterDto.UserName);
            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است ");



            var passwordHash = userRegisterDto.Password.EncodeToMd5();
            _appDbContext.Users.Add(new User()
            {
                FullName = userRegisterDto.FullName,
                UserName = userRegisterDto.UserName,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                Role = User.UserRole.User,
                Password = passwordHash


            });
            _appDbContext.SaveChanges();
            return OperationResult.Success();
        }


        public UserDto LoginUser(UserLoginDto userLoginDto)
        {
            var PasswordHash = userLoginDto.Password.EncodeToMd5();
            var User = _appDbContext.Users.FirstOrDefault(u => u.UserName == userLoginDto.UserName && u.Password == PasswordHash);

            if (User == null)
                return null;

            var UserDto = new UserDto()
            {
                FullName = User.FullName,
                UserName = User.UserName,
                Password = User.Password,
                Role = User.Role,
                CreatedDate = User.CreatedDate,
                Id = User.Id
            };
            return UserDto;


        }


    }

    
}
