using Core.DataAccess.EntityFramework;
using DataAccess.Apstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDto> GetAllProductDetailDtos(Expression<Func<ProductDto, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDto
                             {
                                 ProductId = p.ProductId,
                                 CategoryId = c.CategoryId,
                                 ProductName = p.ProductName,
                                 UnitPrice = p.UnitPrice,
                                 ImagePath = context.ProductImages.FirstOrDefault(x=>x.ProductId==p.ProductId).ProductImage
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList(); 

            }
        }
        public List<ProductUser> GetAllProductUser(Expression<Func<ProductUser, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductUser
                             {
                                 ProductId = p.ProductId,
                                 CategoryId = c.CategoryId,
                                 ProductName = p.ProductName,
                                 UnitPrice = p.UnitPrice,
                                 UserId=p.UserId,
                                 ImagePath = context.ProductImages.FirstOrDefault(x => x.ProductId == p.ProductId).ProductImage
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }

  

        public List<ProductDetailDto> GetProductsCommentDtos(Expression<Func<ProductDetailDto, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products join x in context.Comments on p.ProductId equals x.ProductId
                             join u in context.Users on x.UserId equals u.Id 
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 UserName=u.FirstName + u.LastName,
                                 Comment=x.Comment
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}