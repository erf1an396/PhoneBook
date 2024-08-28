using Microsoft.AspNetCore.Identity;

namespace PhoneBook.DataLayer.Entities;

public class ApplicationUserToken : IdentityUserToken<string>
{
    public virtual ApplicationUser User { get; set; }
}