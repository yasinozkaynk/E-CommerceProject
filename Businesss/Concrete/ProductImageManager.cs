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
        public IResult Add(IFormFile file,ProductImage productImage)
        {

            IResult result = BusinessRules.Run(CheckIfProductImageLimit(productImage.ProductId));
            if (result != null)
            {
                return result;
            }
            productImage.ImagePath = FileHelper.Add(file);
            //productImage.Date = DateTime.Now;
            _productImageDal.Add(productImage);
            return new SuccessResult();
        }

        [SecuredOperation("Admin")]
        public IResult Delete(IFormFile file,ProductImage productImage)
        {
            //FileHelper.Delete(productImage.ImagePath);
            //_productImageDal.Delete(productImage);
            return new SuccessResult();
        }

        [SecuredOperation("Admin")]
        public IResult Update(IFormFile file,ProductImage productImage)
        {
        //    IResult result = BusinessRules.Run();
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    productImage.ImagePath = FileHelper.Update(_productImageDal.Get(p => p.Id ==productImage.Id).ImagePath, file);
        //    productImage.Date = DateTime.Now;
        //    _productImageDal.Update(productImage);
            return new SuccessResult();
        }

        public IDataResult<ProductImage> Get(int id)
        {
            return new SuccessDataResult<ProductImage>(_productImageDal.Get(p => p.Id == id));
        }

        public IDataResult<List<ProductImage>> GetAll()
        {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll());
        }

        public IDataResult<List<ProductImage>> GetImagesByProductId(int id)
        {
            IResult result = BusinessRules.Run(CheckIfProductImageNull(id));

            if (result != null)
            {
                return new ErrorDataResult<List<ProductImage>>(result.Message);
            }

            return new SuccessDataResult<List<ProductImage>>(CheckIfProductImageNull(id).Data);
        }

        private IDataResult<List<ProductImage>> CheckIfProductImageNull(int id)
        {
            try
            {
                string path = @"\wwwroot\uploads\logo.jpg";
                var result = _productImageDal.GetAll(c => c.ProductId == id).Any();
                if (!result)
                {
                    List<ProductImage> carimage = new List<ProductImage>();
                    carimage.Add(new ProductImage { ProductId = id, ImagePath = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<ProductImage>>(carimage);
                }
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<ProductImage>>(exception.Message);
            }

            return new SuccessDataResult<List<ProductImage>>( _productImageDal.GetAll(p=>p.Id==id).ToList());
           
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
