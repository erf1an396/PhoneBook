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

            

            if (contacts == null || !contacts.Any())
            {
                return NotFound();
            }

            return Json(contacts);
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

        //[HttpPost]
        //public JsonResult Add(ContactDto contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Code to add the contact to the database
        //        // For example, using Entity Framework
        //        _appDbContext.Contacts.Add(new Contact
        //        {
        //            Name = contact.Name,
        //            PhoneNumber = contact.PhoneNumber,
        //            Email = contact.Email,
        //            CreatedAt = DateTime.Now
        //        });
        //        _appDbContext.SaveChanges();

        //        return Json(new { success = true });
        //    }

        //    return Json(new { success = false, message = "Invalid data" });
        //}


        [HttpPut]
        public async Task<IActionResult> EditAjax([FromBody] EditContactDto contactDto)
        {
            if (ModelState.IsValid)
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
    }
}
