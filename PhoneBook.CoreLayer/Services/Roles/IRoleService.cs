using PhoneBook.CoreLayer.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();

        Task AddRoleAsync(RoleDto roleDto);

        Task DeleteRoleAsync(int id);


    }
}
