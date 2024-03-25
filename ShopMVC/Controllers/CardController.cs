using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Helper;

namespace ShopMVC.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
        public IActionResult Index()
        {
            return View(_cardService.GetProducts());
        }
        public IActionResult Add(int id)
        {
            _cardService.Add(id);
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Remove(int id)
        {
            _cardService.Remove(id);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
