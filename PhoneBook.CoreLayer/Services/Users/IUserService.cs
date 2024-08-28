using PhoneBook.CoreLayer.Utilities;
using PhoneBook.CoreLayer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.CoreLayer.Services.Users
{
    public interface IUserService
    {
        Task <IdentityResult> RegisterUserAsync(UserRegisterDto userRegister);
        Task <UserDto> LoginUserAsync(UserLoginDto userLogin);

        


    }
}
