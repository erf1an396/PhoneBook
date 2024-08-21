using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.CoreLayer.DTOs.Contacts;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.CoreLayer.Services.Contacts;
using PhoneBook.DataLayer.Context;
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


        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 
            var contacts = await _contactService.GetContactsAsync(userId);

            //if (contacts == null || !contacts.Any())
            //{
            //    return NotFound();
            //}
            
            return Json(contacts);
        }

        public async Task<IActionResult>GetContactByIdAjax(int Id)
        {
            var contact = await _contactService.GetContactByIdAsync(Id);

            return Json(contact);

        }


        [HttpPost]
        
        public async Task<IActionResult> CreateAjax([FromBody] CreateContactDto contactDto)
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            

            if (ModelState.IsValid && contactDto.IsValidPhoneNumber() && contactDto.IsValidName() )
            {
                await _contactService.AddContactAsync(contactDto , userId);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpPut]
        public async Task<IActionResult> EditAjax([FromBody] EditContactDto contactDto)
        {
            if (ModelState.IsValid && contactDto.IsValidPhoneNumber() && contactDto.IsValidName())
            {
                await _contactService.UpdateContactAsync(contactDto);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAjax(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return Json(new { success = true });
        }


        [HttpGet]
        public IActionResult Search(string searchText)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            

            var contacts = _contactService.SearchContactsByName(searchText, userId);

            return Json(contacts);
        }
    }
}
