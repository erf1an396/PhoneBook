using Microsoft.AspNetCore.Identity;

namespace PhoneBook.DataLayer.Entities;

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public virtual ApplicationRole Role { get; set; }
}