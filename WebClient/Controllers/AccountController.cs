using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult SignOut(string page)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
