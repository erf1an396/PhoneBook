using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Entities
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        

        public int ContactId { get; set; }

        public string Address { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}
