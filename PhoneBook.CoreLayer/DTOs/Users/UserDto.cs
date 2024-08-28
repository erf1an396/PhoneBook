using PhoneBook.CoreLayer.Utilities;
using PhoneBook.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PhoneBook.DataLayer.Entities.ApplicationUser;

namespace PhoneBook.CoreLayer.DTOs.Users
{
    public class UserDto
    {

        public string Id { get; set; }

        
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }


        public List<string> Role { get; set; } = new List<string>();

        public DateTime CreatedDate { get; set; }

        public OperationResultStatus Status { get; set; }
    }
}
