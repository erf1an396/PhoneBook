using Microsoft.EntityFrameworkCore;
using PhoneBook.CoreLayer.DTOs.Roles;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace PhoneBook.CoreLayer.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManger;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManger = roleManager;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = _roleManger.Roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            });

            return await Task.FromResult(roles.ToList());
        }

        public async Task<IdentityResult> AddRoleAsync(RoleDto roleDto)
        {
            var role = new ApplicationRole
            {
                Name = roleDto.Name
            };

            return await _roleManger.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            var role = await _roleManger.FindByIdAsync(id);
            if(role != null)
            {
                return await _roleManger.DeleteAsync(role);
            }

            return IdentityResult.Failed(new IdentityError
            {
                Description = $"Role with ID {id} not found."
            });
        }
    }
}
