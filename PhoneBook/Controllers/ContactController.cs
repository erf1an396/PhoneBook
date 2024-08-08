using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.CoreLayer.DTOs.Contacts;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.CoreLayer.Services.Contacts;
using PhoneBook.DataLayer.Entities;
using System.Security.Claims;

namespace PhoneBook.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
           
        }


        public async Task<IActionResult> Index()
        {
            
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
            if (int.TryParse(userIdString, out int userId))
            {
            
                var contacts = await _contactService.GetContactsAsync(userId);
                return View(contacts);
            }

            return BadRequest("Invalid user ID.");
        }


        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] CreateContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddContactAsync(contactDto);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody] EditContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                await _contactService.UpdateContactAsync(contactDto);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return Json(new { success = true });
        }
    }
}
