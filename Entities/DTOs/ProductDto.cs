
using Core.Entities;

namespace Entities.DTOs
{
    public class ProductDto:IEntity
    {
        public int ProductId { get; set; }       
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
  

    }
}