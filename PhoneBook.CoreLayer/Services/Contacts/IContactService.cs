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
        Task<IEnumerable<ContactDto>> GetContactsAsync(int userId);
        Task<ContactDto> GetContactByIdAsync(int id);
        Task AddContactAsync(CreateContactDto contactDto , int userId);
        Task UpdateContactAsync(EditContactDto contactDto);
        Task DeleteContactAsync(int id);


        IEnumerable<ContactDto> SearchContactsByName(string searchText , int userId);
    }
}
