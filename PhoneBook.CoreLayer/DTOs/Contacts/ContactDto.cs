using PhoneBook.CoreLayer.DTOs.Users;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.DTOs.Contacts
{
    public class ContactDto
    {
        public string Id { get; set; }

        public string  Name { get; set; }

        

        public bool IsDeleted { get; set; }

        
       

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserDto User { get; set; }

        public bool IsValidName()
        {
            return !string.IsNullOrWhiteSpace(Name) && Name.Length >= 5;
        }

        public bool IsValidPhoneNumber()
        {
            return PhoneNumbers.All(p => p.Length == 10 && p.All(char.IsDigit) && p[0] != '0');
        }

        
        public List<string>  PhoneNumbers { get; set; } 

        public List<string> Emails { get; set; } 

    }
}
