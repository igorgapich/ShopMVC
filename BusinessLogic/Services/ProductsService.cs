using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ShopMVCDbContext _context;
        public ProductsService(ShopMVCDbContext context)
        {
            _context = context;
        }
        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public Product? Get(int? id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }
        public List<Product> GetAll()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }


        public void Edit(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
