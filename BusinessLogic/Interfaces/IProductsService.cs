using BusinessLogic.DTOs;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IProductsService
    {
        List<CategoryDto> GetAllCategories();
        List<ProductDto> GetAll();
        //List<Product> GetByIds(int[] listId);
        List<ProductDto> GetByOrderPrice();
        ProductDto? Get(int? id);
        EditProductDto? GetEditProductDto(int? id);
        void Create(CreateProductDto product);
        void Edit(EditProductDto productDto);
        void Delete(int? id);
    }
}
