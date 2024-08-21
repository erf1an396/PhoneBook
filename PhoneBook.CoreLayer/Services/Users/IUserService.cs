using PhoneBook.CoreLayer.Utilities;
using PhoneBook.CoreLayer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.Services.Users
{
    public interface IUserService
    {
        Task <OperationResult> RegisterUser(UserRegisterDto userRegister);
        UserDto LoginUser(UserLoginDto userLogin);

        


    }
}
