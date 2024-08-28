using Microsoft.AspNetCore.Identity;

namespace PhoneBook.DataLayer.Entities;

public class ApplicationUserClaim : IdentityUserClaim<string>
{
    public virtual ApplicationUser User { get; set; }
}