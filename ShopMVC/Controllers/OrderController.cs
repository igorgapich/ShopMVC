using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC.Helper;
using System.Security.Claims;
using System.Text.Json;

namespace ShopMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly IMailService _mailService;

        public OrderController(IOrdersService ordersService, IMailService mailService)
        {
            _ordersService = ordersService;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _ordersService.GetAll(userId);
            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userName = User.FindFirstValue(ClaimTypes.Name);
            List<int> idList = HttpContext.Session.GetObject<List<int>>("mycart");
            _ordersService.Create(userId, idList);
            HttpContext.Session.Remove("mycart");
            var orders = _ordersService.GetAll(userId);
            string text = "";
            foreach (var item in orders)
            {
                text += $"{item.Id} {item.OrderDate}  {item.TotalPrice}\n";

            }
            await _mailService.SendMailAsync("Your Order", text, "hohalax367@searpen.com");
            await _mailService.SendMailAsync("Your Order", text, userName);
            return RedirectToAction(nameof(Index));
        }
    }
}