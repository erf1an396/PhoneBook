using PhoneBook.CoreLayer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.CoreLayer.DTOs.Contacts
{
    public class ContactDto
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public string PhoneNumber { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserDto User { get; set; }

    }
}
