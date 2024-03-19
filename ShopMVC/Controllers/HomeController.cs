using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            List<Category> categories = _context.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, Name = "All", Description = "All Products" });
            ViewBag.ListCategories = categories;
            ViewData["ListCategories"] = categories;

            var products = _context.Products.Include(product => product.Category).ToList();
            if (category_Id != null && category_Id > 0)
            {
                products = products.Where(p => p.CategoryId == category_Id).ToList();
            }
            if (category_Id == null)
            {
                ViewBag.ActiveCategoryId = 0;
            }
            else
            {
                ViewBag.ActiveCategoryId = category_Id;
            }

            return View(products);

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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}