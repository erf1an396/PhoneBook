using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Entities
{
    public  class Contact
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }


        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public ICollection<Email> Emails { get; set; } = new List<Email>();



        #endregion
    }
}
