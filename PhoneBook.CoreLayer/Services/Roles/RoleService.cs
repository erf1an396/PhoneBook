using Microsoft.EntityFrameworkCore;
using PhoneBook.CoreLayer.DTOs.Roles;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhoneBook.CoreLayer.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _appDbContext;

        public RoleService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return await _appDbContext.Roles
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,  

                }).ToListAsync();
        }

        public async Task AddRoleAsync(RoleDto roleDto)
        {
            var role = new Role
            {
                Name = roleDto.Name
            };
            _appDbContext.Roles.Add(role);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _appDbContext.Roles.FindAsync(id);  
            if(role != null)
            {
                _appDbContext.Roles.Remove(role);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
