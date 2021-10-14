using Business.Apstract;
using Business.BusinessAspects.Autoface;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Businesss.Apstract;
using Core.Aspects.Autofac.Chacing;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Busines;
using Core.Utilities.Results;
using DataAccess.Apstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{ 
    // bir entity manager kendisi hariç başka bir dalı enjekte edemez'!!!!!!!!!!!!!!!
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
 
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            ////IResult result = BusinessRules.Run
            //    (CheckIfProductNameExist(product.ProductName),
            //     CheckIfProductCountCetegoryCorrect(product.CategoryId));
            //     CheckIfCategoryLimitExceded();

            //if (result!=null)
            //{
            //    return result;
            //}

            _productDal.Add(product);
            return new SuccessResult(ProductMessages.ProductAdded);

        }


        public IDataResult<List<ProductDto>> GetAll()
        {
            IResult resullt = BusinessRules.Run();
            if (resullt != null)
            {
                return (IDataResult<List<ProductDto>>)resullt;
            }

            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetailDtos(), ProductMessages.ProductsListed);
        }


        public IDataResult<List<ProductDto>> GetAllByCategoryId(int categoryId )
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetailDtos(p =>p.CategoryId == categoryId));
        }


        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice < max));
        }

        public IDataResult<List<ProductDto>> productDetaDtos(int productId)
        {

            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetailDtos(p=>p.ProductId==productId));
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountCetegoryCorrect(product.CategoryId).Success)
            {
                _productDal.Update(product);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductCountCetegoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 30)
            {
                return new ErrorResult(ProductMessages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result == true)
            {
                return new ErrorResult(ProductMessages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>100)
            {
                return new ErrorResult(ProductMessages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<10)
            {
                throw new Exception();
            }
            Add(product);
            return null;
        }

        public IDataResult<List<Product>> GetAllById(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        }

        public IDataResult<List<ProductDto>> GetByUnitsPrice()
        {
            return new SuccessDataResult<List<ProductDto>>(_productDal.GetAllProductDetailDtos(x=>x.UnitPrice<=30));

        }

        public IDataResult<List<ProductDetailDto>> productCommentDtos(int productId)
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsCommentDtos(x=>x.ProductId==productId));
        }

        public IDataResult<List<ProductUser>> UserId(int userId)
        {
            return new SuccessDataResult<List<ProductUser>>(_productDal.GetAllProductUser(x => x.UserId == userId));
        }
    }
}
