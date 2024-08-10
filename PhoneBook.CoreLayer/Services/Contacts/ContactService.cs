using PhoneBook.CoreLayer.DTOs.Contacts;
using PhoneBook.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using PhoneBook.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PhoneBook.CoreLayer.Services.Contacts
{
    public class ContactService : IContactService

    {
        private readonly AppDbContext _context;
        private readonly IUserContextService _userContextService;
        
        public ContactService(AppDbContext context, IUserContextService userContextService)
        {
            
            _context = context;
            _userContextService = userContextService;
            
        }

        

        public async Task<IEnumerable<ContactDto>> GetContactsAsync(int userId)
        {
            var userId1 = _userContextService.GetUserId();
            var userId2 = int.Parse(userId1);
            userId = userId2;

            return await _context.Contacts
                .Where(c => c.UserId == userId)
                .Select(c => new ContactDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email ,
                    UserId = c.UserId,
                    IsDeleted = false , 
                    CreatedAt = c.CreatedAt,
           


                })
                .ToListAsync();
        }

        public async Task<ContactDto> GetContactByIdAsync(int id)
        {
            

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return null;

            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                UserId = contact.UserId,
                IsDeleted = false,
                CreatedAt = contact.CreatedAt,
            };
        }

        public async Task AddContactAsync(CreateContactDto contactDto)
        {
            var userId = _userContextService.GetUserId();
            var userId2 = int.Parse(userId);
            


            var contact = new Contact
            {
                Name = contactDto.Name,
                PhoneNumber = contactDto.PhoneNumber,
                Email = contactDto.Email,
                UserId = userId2, 
                IsDeleted = false,
                Id = contactDto.ContactId

            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(EditContactDto contactDto)
        {
            var contact = await _context.Contacts.FindAsync(contactDto.Id);
            if (contact == null) return;

            contact.Name = contactDto.Name;
            contact.PhoneNumber = contactDto.PhoneNumber;
            contact.Email = contactDto.Email;

            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        //public async Task<IEnumerable<ContactDto>> GetContactByNameAsync(string name , int userId)
        //{
        //    var userId1 = _userContextService.GetUserId();
        //    var userId2 = int.Parse(userId1);
        //    userId = userId2;

        //     return  await _context.Contacts
        //        .Where(c => c.UserId == userId && c.Name == name)
        //        .Select(c => new ContactDto
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            PhoneNumber = c.PhoneNumber,
        //            Email = c.Email,
        //            UserId = c.UserId,
        //            IsDeleted = false,
        //            CreatedAt = c.CreatedAt,



        //        }).ToListAsync();



        //}

        public IEnumerable<ContactDto> SearchContactsByName(string searchText , int userId  )
        {

           
                return _context.Contacts
                    .Where(c => c.Name.Contains(searchText) && c.UserId == userId)
                    .Select(c => new ContactDto
                    {
                        Name = c.Name,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email
                    }).ToList();
            

            
            
            
        }


    }
}
