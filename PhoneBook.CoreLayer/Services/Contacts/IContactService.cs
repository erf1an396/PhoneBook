using PhoneBook.CoreLayer.DTOs.Contacts;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.Services.Contacts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetContactsAsync(string userId);
        Task<ContactDto> GetContactByIdAsync(string id);
        Task AddContactAsync(CreateContactDto contactDto , string userId);
        Task UpdateContactAsync(EditContactDto contactDto);
        Task DeleteContactAsync(string id);


        IEnumerable<ContactDto> SearchContactsByName(string searchText , string userId);
    }
}
