using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
