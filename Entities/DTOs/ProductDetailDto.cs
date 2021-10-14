
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ProductDetailDto : IEntity
    {
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }

    }
}