using PhoneBook.CoreLayer.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PhoneBook.CoreLayer.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();

        Task<IdentityResult> AddRoleAsync(RoleDto roleDto);

        Task<IdentityResult> DeleteRoleAsync(string id);


    }
}
