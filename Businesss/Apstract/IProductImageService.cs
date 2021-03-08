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
        IResult Add(IFormFile file ,ProductImage productImage);
        IResult Delete(IFormFile file,ProductImage productImage);
        IResult Update(IFormFile file, ProductImage productImage);
        IDataResult<ProductImage>Get(int id);
        IDataResult<List<ProductImage>> GetAll();
        IDataResult<List<ProductImage>> GetImagesByProductId(int id);

    }
}
