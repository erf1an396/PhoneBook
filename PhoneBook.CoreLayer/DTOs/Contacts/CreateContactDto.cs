using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.DTOs.Contacts
{
    public class CreateContactDto
    {

        public int ContactId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsValidPhoneNumber()
        {
            return PhoneNumber.Length == 10 && PhoneNumber.All(char.IsDigit);
        }

        public int UserId { get; set; }

        
    }
}
