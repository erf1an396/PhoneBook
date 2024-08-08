using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Entities
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string FullName { get; set; }

        public bool IsDeleted { get; set; }


        public UserRole Role { get; set; }

        public enum UserRole
        {
            Admin,
            User
        }

        #region Relations
        public ICollection<Contact> Contacts { get; set; }

        #endregion
    }
}
