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
        
        public ContactService(AppDbContext context)
        {
            
            _context = context;
            
        }

        

        public async Task<IEnumerable<ContactDto>> GetContactsAsync(int userId)
        {
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
            var contact = new Contact
            {
                Name = contactDto.Name,
                PhoneNumber = contactDto.PhoneNumber,
                Email = contactDto.Email,
                UserId = contactDto.UserId, 
                IsDeleted = false,
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

        
    }
}
