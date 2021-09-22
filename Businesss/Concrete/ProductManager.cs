﻿using Business.Apstract;
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
            IResult result = BusinessRules.Run
                (CheckIfProductNameExist(product.ProductName),
                 CheckIfProductCountCetegoryCorrect(product.CategoryId));
                 CheckIfCategoryLimitExceded();

            if (result!=null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(ProductMessages.ProductAdded);

        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 6)
            {
                return new ErrorDataResult<List<Product>>(SystemMessages.MaintenanceTime);
            }
            Thread.Sleep(3000);

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), ProductMessages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId )
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        }

        [PerformanceAspect(10)]//bu method 10 saniye üstünde çalışırsa benbi uyar
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice < max));
        }

        public IDataResult<List<ProductDetailDto>> productDetaDtos()
        {

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetailDtos());
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
    }
}
