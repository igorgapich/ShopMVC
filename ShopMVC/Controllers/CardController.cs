using Microsoft.AspNetCore.Mvc;

namespace ShopMVC.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
