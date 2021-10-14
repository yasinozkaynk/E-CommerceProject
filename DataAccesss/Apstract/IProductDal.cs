
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Apstract
{
    public interface IProductDal : IEntityRepostory<Product>
    {
        List<ProductDto> GetAllProductDetailDtos(Expression<Func<ProductDto, bool>> filter = null);
        List<ProductUser> GetAllProductUser(Expression<Func<ProductUser, bool>> filter = null);
        List<ProductDetailDto> GetProductsCommentDtos(Expression<Func<ProductDetailDto, bool>> filter);
    }
}
