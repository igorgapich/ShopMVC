using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Helper;
using ShopMVC.Models;
using System.Diagnostics;

namespace ShopMVC.Controllers
{
    //[Controller]
    //public class Home
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly List<Product> _products;
        private readonly ShopMVCDbContext _context;

        public HomeController(ILogger<HomeController> logger, ShopMVCDbContext context)
        {
            _logger = logger;
            _context = context;            
            //_products = SeedData.Products;
        }

        //ViewData
        //ViewBag
        //Mode view
        // 
        public IActionResult Index(int? category_Id)
        {
            //Example using Cookies
            //HttpContext.Response.Cookies.Append("name", "Igor");
            //Get Cookies
            //ViewBag.NameAuthor = HttpContext.Request.Cookies["name"];
            //Delete Cookies
            //HttpContext.Response.Cookies.Delete("name");
            List<Category> categories = _context.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, Name = "All", Description = "All Products" });
            ViewBag.ListCategories = categories;
            ViewData["ListCategories"] = categories;

            var products = _context.Products.Include(product => product.Category).ToList();
            if (category_Id != null && category_Id > 0)
            {
                products = products.Where(p => p.CategoryId == category_Id).ToList();
            }

            var productsCardViewModel = products.Select(
                p => new ProductCardViewModel
                {
                    Product = p,
                    IsInCard = IsProductInCard(p.Id)
                }).ToList();

            if (category_Id == null)
            {
                ViewBag.ActiveCategoryId = 0;
            }
            else
            {
                ViewBag.ActiveCategoryId = category_Id;
            }

            return View(productsCardViewModel);
        }

        private bool IsProductInCard(int id)
        {
            List<int> idList = HttpContext.Session.GetObject<List<int>>("mycard");
            if(idList == null) return false;
            return idList.Contains(id);
        }

        //public string Index(string name="Guest")
        //{
        //   return $"Hello, {name}";
        //}


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            var services = HttpContext.RequestServices.GetService<UserManager<User>>();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}