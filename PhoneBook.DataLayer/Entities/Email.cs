using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.DataLayer.Entities
{
    public class Email
    {
        public string Id { get; set; }
        

        public string ContactId { get; set; }

        public string Address { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}
