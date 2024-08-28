using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PhoneBook.DataLayer.Entities.ApplicationUser;

namespace PhoneBook.CoreLayer.DTOs.Users
{
    public class UserEditDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public List<string> RoleIds {  get; set; } 
    }
}
