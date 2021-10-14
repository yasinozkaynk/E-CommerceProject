using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Apstract
{
    public interface IProductImageService
    {
        IResult Add(IFormFile file ,ProductImagePath productImage);
        IResult Delete(IFormFile file,ProductImagePath productImage);
        IResult Update(IFormFile file, ProductImagePath productImage);
        IDataResult<ProductImagePath>Get(int id);
        IDataResult<List<ProductImagePath>> GetAll();
        IDataResult<List<ProductImagePath>> GetImagesByProductId(int id);

    }
}
