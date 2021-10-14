using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Businesss.Apstract
{
    public interface IProductService
    {

        IDataResult<List<ProductDto>> GetAll();
        IDataResult<List<ProductDto>> GetByUnitsPrice();
        IDataResult<List<ProductDto>> GetAllByCategoryId(int categoryId);
        IDataResult<List<Product>> GetAllById(int categoryId);
        IDataResult<List<ProductDto>> productDetaDtos(int productId);
        IDataResult<List<ProductUser>> UserId(int userId);
        IDataResult<List<ProductDetailDto>> productCommentDtos(int productId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product);

    }
}
