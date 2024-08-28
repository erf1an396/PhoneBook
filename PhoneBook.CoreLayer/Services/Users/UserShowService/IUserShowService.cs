using PhoneBook.CoreLayer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.Services.Users.UserShowService
{
    public interface IUserShowService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task DeleteUserAsync(string id);

        Task UpdateUserAsync(UserEditDto userEditDto);

        Task<UserDto> GetUserByIdAsync(string id);
    }
}
