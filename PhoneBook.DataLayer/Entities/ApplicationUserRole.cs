using Microsoft.AspNetCore.Identity;

namespace PhoneBook.DataLayer.Entities;

public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}