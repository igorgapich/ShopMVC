using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMVC.Models;

namespace ShopMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopMVCDbContext _context;
        public ProductController(ShopMVCDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            ViewBag.ListCategories = categories;
            ViewData["ListCategories"] = categories;
            //TODO: dbcontext
            var products = _context.Products.Include(p=>p.Category).ToList();
            return View(products);
        }
        public IActionResult Details(int? id, string returnUrl = null)
        {
            //find in DataBase
            var product = _context.Products.Include(p=>p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            ViewBag.ReturnUrl = returnUrl;
            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            //find in DataBase
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();

            }
            return RedirectToAction(nameof(Index),"Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            ViewBag.ListCategory = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                var categories = _context.Categories.ToList();
                ViewBag.ListCategory = new SelectList(categories, "Id", "Name");
                return View(product);
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            var product = _context.Products.Include(p=>p.Category).FirstOrDefault(p=>p.Id == id);
            if (product != null)
            {
                var categories = _context.Categories.ToList();
                ViewBag.ListCategory = new SelectList(categories, "Id", "Name", product.CategoryId);
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}