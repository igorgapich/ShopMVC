using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        //private readonly ShopMVCDbContext _context;
        public ProductsService(IRepository<Product> productRepo,
                                IRepository<Category> categoryRepo,
                                IMapper mapper,
                                IFileService fileService)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _fileService = fileService;
        }
        public void Create(CreateProductDto productDto)
        {
            //save img to server
            string imagePath = _fileService.SaveProductImage(productDto.Image).Result;
            Product product = new Product()
            {
                //Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                ImagePath = imagePath,
                CategoryId = productDto.CategoryId
            };

            //var product = _mapper.Map<Product>(createProductDto); //ProductDto => Product (Entity)
            _productRepo.Insert(product);
            _productRepo.Save();
        }

        public void Delete(int? id)
        {
            var product = _productRepo.GetByID(id);
            _fileService.DeleteProductImage(product.ImagePath);
            if (product != null)
            {
                _productRepo.Delete(product);
                _productRepo.Save();
            }
        }
        public void Edit(EditProductDto productDto)
        {
            //delete oldfile from "images"
            var productOld = Get(productDto.Id);
            if (productOld != null)
            {
                //save new file
                _fileService.DeleteProductImage(productOld.ImagePath);
                //save image to server
                string imagePath = _fileService.SaveProductImage(productDto.Image).Result;
                productDto.ImagePath = imagePath;

                var product = _mapper.Map<Product>(productDto);
                _productRepo.Update(product);
                _productRepo.Save();
            }
        }
        public ProductDto? Get(int? id)
        {
            //return _productRepo.GetByID(id);
            //return GetAll().FirstOrDefault(x => x.Id == id);
            //return _productRepo.Get(includeProperties: new[] { "Category" }).Where(p=>p.Id == id).SingleOrDefault();
            //return _productRepo.Get(filter: x=> x.Id == id, includeProperties: new[] { "Category" }).SingleOrDefault();

            //using custom mapping Product => object ProductDto
            //get from DB with Repository
            var product = _productRepo.Get(filter: x=> x.Id == id, includeProperties: new[] { "Category" }).SingleOrDefault();
            /*
            ProductDto productDto = new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };
            var productDto = _mapper.Map<Product>(productDto);
            */
            return _mapper.Map<ProductDto>(product);
           
        }

        public List<ProductDto> GetAll()
        {
            //include properties: LEFT JOIN  in SQL
            //return _productRepo.Get(includeProperties: new[] { "Category" }).ToList();

            var products = _productRepo.Get(includeProperties: new[] { "Category" }).ToList();

            //return products.Select(product => new ProductDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    Price = product.Price,
            //    ImagePath = product.ImagePath,
            //    CategoryId = product.CategoryId,
            //    CategoryName = product.Category.Name
            //}).ToList();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public List<CategoryDto> GetAllCategories()
        {
            //return _categoryRepo.Get().ToList();
            var categories = _categoryRepo.Get();
            return categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description

            }).ToList();
        }

        public List<ProductDto> GetByOrderPrice()
        {
            var products = _productRepo.Get(orderBy: q => q.OrderBy(p => p.Price), includeProperties: new[] { "Category" }).ToList();

            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            }).ToList();
        }

        public EditProductDto? GetEditProductDto(int? id)
        {
            var product = Get(id);

            return new EditProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
            };
        }
    }
}
