using Business.Apstract;
using Core.Utilities.Results;
using DataAccess.Apstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Core.Utilities.Busines;
using Business.BusinessAspects.Autoface;
using Core.Aspects.Autofac.Validation;
using Business.Constants;
using Core.Helpers;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;

        public ProductImageManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }

       // [SecuredOperation("carImages.add,admin")]
        public IResult Add(IFormFile file,ProductImagePath productImage)
        {
            //IResult result = BusinessRules.Run(CheckIfProductImageLimit(productImage.ProductId));
            //if (result != null)
            //{
            //    return result;
            //}
            productImage.ProductImage = FileHelper.Add(file);
            productImage.Date = DateTime.Now;
            _productImageDal.Add(productImage);
            return new SuccessResult(ProductImageMessage.PictureAdded);
        }

        [SecuredOperation("Admin")]
        public IResult Delete(IFormFile file,ProductImagePath productImage)
        {
            FileHelper.Delete(productImage.ProductImage);
            _productImageDal.Delete(productImage);
            return new SuccessResult();
        }

        [SecuredOperation("Admin")]
        public IResult Update(IFormFile file,ProductImagePath productImage)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            productImage.ProductImage = FileHelper.Update(_productImageDal.Get(p => p.Id ==productImage.Id).ProductImage, file);
            productImage.Date = DateTime.Now;
            _productImageDal.Update(productImage);
            return new SuccessResult();
        }

        public IDataResult<ProductImagePath> Get(int id)
        {
            return new SuccessDataResult<ProductImagePath>(_productImageDal.Get(p => p.Id == id));
        }

        public IDataResult<List<ProductImagePath>> GetAll()
        {
            return new SuccessDataResult<List<ProductImagePath>>(_productImageDal.GetAll());
        }

        public IDataResult<List<ProductImagePath>> GetImagesByProductId(int id)
        {
            IResult result = BusinessRules.Run(CheckIfProductImageNull(id));

            if (result != null)
            {
                return new ErrorDataResult<List<ProductImagePath>>(result.Message);
            }

            return new SuccessDataResult<List<ProductImagePath>>(CheckIfProductImageNull(id).Data);
        }
        private IDataResult<List<ProductImagePath>> CheckIfProductImageNull(int id)
        {
            try
            {
                string path = @"\wwwroot\uploads\logo.jpg";
                var result = _productImageDal.GetAll(c => c.ProductId == id).Any();
                if (!result)
                {
                    List<ProductImagePath> productImages = new List<ProductImagePath>();
                    productImages.Add(new ProductImagePath { ProductId = id, ProductImage = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<ProductImagePath>>(productImages);
                }
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<ProductImagePath>>(exception.Message);
            }

            return new SuccessDataResult<List<ProductImagePath>>( _productImageDal.GetAll(p=>p.Id==id).ToList());
           
        }

        private IResult CheckIfProductImageLimit(int productId)
        {
            var imagecount = _productImageDal.GetAll(p=>p.ProductId==productId).Count;
            if (imagecount>=5)
            {
                return new ErrorResult(ProductImageMessage.ProductImageLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
