using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.DTOs.Contacts
{
    public class EditContactDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsValidPhoneNumber()
        {
            return PhoneNumber.Length == 10 && PhoneNumber.All(char.IsDigit);
        }
    }
}
