using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.CoreLayer.DTOs.Contacts;
using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.CoreLayer.Services.Contacts;
using PhoneBook.DataLayer.Context;
using PhoneBook.DataLayer.Entities;
using System.Data;
using System.Security.Claims;


namespace PhoneBook.Controllers
{


    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly UserManager<ApplicationUser> _userManger;




        public ContactController(IContactService contactService, UserManager<ApplicationUser> userManger)
        {
            _contactService = contactService;
            _userManger = userManger;


        }


        [HttpGet]
        
        public async Task<IActionResult> GetContacts()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var contacts = await _contactService.GetContactsAsync(userId);

            //if (contacts == null || !contacts.Any())
            //{
            //    return NotFound();
            //}

            return Json(contacts);
        }

        public async Task<IActionResult> GetContactByIdAjax(string Id)
        {
            var contact = await _contactService.GetContactByIdAsync(Id);

            return Json(contact);
        }


        [HttpPost]
        [Authorize(Roles = "AddContact")]
        public async Task<IActionResult> CreateAjax([FromBody] CreateContactDto contactDto)
        {

            var user = await _userManger.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, errors = "User not found." });
            }

            contactDto.UserId = user.Id.ToString();


            //var errors = ModelState.Values.SelectMany(value => value.Errors)
            //    .Select(error => error.ErrorMessage)
            //    .ToList();
            //return Json(new { success = false, errors });


            if (contactDto.IsValidPhoneNumber() && contactDto.IsValidName())
            {
                await _contactService.AddContactAsync(contactDto, user.Id.ToString());
                return Json(new { success = true });
            }

            return Json(new
            { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(s => s.ErrorMessage) });
        }


        [HttpPut]
        [Authorize(Roles = "UpdateContact")]
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
        [Authorize(Roles = "DeleteContact")]
        public async Task<IActionResult> DeleteAjax(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return Json(new { success = true });
        }


        [HttpGet]
        public IActionResult Search(string searchText)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var contacts = _contactService.SearchContactsByName(searchText, userId);

            return Json(contacts);
        }
    }
}
