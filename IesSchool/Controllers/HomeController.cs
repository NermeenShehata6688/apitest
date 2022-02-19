using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    
    public class HomeController : Controller
    {
       // [Route("{asswordResetDto:PasswordResetDto}")]
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult rest()
        {

            return View();
        }
    }
}
