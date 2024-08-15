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

        public bool IsValidName()
        {
            return !string.IsNullOrWhiteSpace(Name) && Name.Length >= 5;
        }


        public bool IsValidPhoneNumber()
        {
            return PhoneNumbers.All(p => p.Length == 10 && p[0] != '0' && p.All(char.IsDigit));
        }

        public int UserId { get; set; }


        public List<string> PhoneNumbers { get; set; } = new List<string>();
        public List<string> Emails { get; set; } = new List<string>();


    }
}
