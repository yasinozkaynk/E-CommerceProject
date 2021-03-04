using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Apstract
{
    public interface IProductService
    {
        List<Product> GetAll();
    }
}
