using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PhoneBook.DataLayer.Entities;

public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    [Key]
    public virtual ApplicationUser User { get; set; }
}