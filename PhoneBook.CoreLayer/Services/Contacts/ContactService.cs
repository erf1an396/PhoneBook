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
            

            var contact = await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Emails)
                .Where(c => c.UserId == userId && c.IsDeleted == false).ToListAsync();

            return contact.Select(c => new ContactDto
            {
                Id = c.Id,
                Name = c.Name,
                UserId = c.UserId,
                IsDeleted = false,
                CreatedAt = c.CreatedAt,
                PhoneNumbers = c.PhoneNumbers.Select(p => p.Number).ToList(),
                Emails = c.Emails.Select(e => e.Address).ToList(),
                 
            });


        }

        public async Task<ContactDto> GetContactByIdAsync(int id)
        {

            var contact =  await _context.Contacts
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Emails)
                .FirstOrDefaultAsync(c  => c.Id == id);
            

            if (contact == null) return null;

            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumbers = contact.PhoneNumbers.Select(p => p.Number).ToList(),
                Emails = contact.Emails.Select(e => e.Address).ToList(),
                UserId = contact.UserId,
                IsDeleted = false,
                CreatedAt = contact.CreatedAt,
            };
        }

        public async Task AddContactAsync(CreateContactDto contactDto , int userId)
        {

            var contact = new Contact
            {
                Name = contactDto.Name,
                
                UserId = userId,
                IsDeleted = false,
                Id = contactDto.ContactId

            };

            foreach (var phoneNumber in contactDto.PhoneNumbers)
            {
                contact.PhoneNumbers.Add(new PhoneNumber { Number = phoneNumber });
            }

            foreach (var email in contactDto.Emails)
            {
                contact.Emails.Add(new Email { Address = email });
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(EditContactDto contactDto)
        {
            var contact = await _context.Contacts
            .Include(c => c.PhoneNumbers)
            .Include(c => c.Emails)
            .FirstOrDefaultAsync(c => c.Id == contactDto.Id);

            if (contact == null) return;


            contact.Name = contactDto.Name;


            contact.PhoneNumbers.Clear();
            foreach (var phoneNumberr in contactDto.PhoneNumbers) 
            {
                contact.PhoneNumbers.Add(new PhoneNumber { Number = phoneNumberr });
            }
            
            contact.Emails.Clear();
            foreach (var email in contactDto.Emails)
            {
                contact.Emails.Add(new Email { Address = email });
            }

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
                    .Where(c => (c.Name.Contains(searchText) || string.IsNullOrEmpty(searchText)) && c.UserId == userId)
                    .Select(c => new ContactDto
                    {
                        Name = c.Name,
                        PhoneNumbers = c.PhoneNumbers.Select(p => p.Number).ToList(),
                        Emails = c.Emails.Select(e => e.Address).ToList()
                    }).ToList();




        }


    }
}
