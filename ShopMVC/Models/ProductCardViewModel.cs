using DataAccess.Entities;

namespace ShopMVC.Models
{
    public class ProductCardViewModel
    {
        public Product Product { get; set; }
        public bool IsInCard {  get; set; }
    }
}
