using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.CoreLayer.DTOs.Roles;
using PhoneBook.CoreLayer.Services.Roles;

namespace PhoneBook.Controllers
{
    
    public class RoleController : Controller
    {
        public readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllRoles()
        {
            var role = await _roleService.GetAllRolesAsync();
            return Json(role);
        }

        [HttpPost]
        
        public async Task<IActionResult> AddRole(RoleDto roleDto)
        {
            
            await _roleService.AddRoleAsync(roleDto);
            return Ok();
        }

        [HttpDelete]
        
        public async Task<IActionResult> DeleteRole(string id)
        {
            await _roleService.DeleteRoleAsync(id);
            return Ok();
        }
    }
}
