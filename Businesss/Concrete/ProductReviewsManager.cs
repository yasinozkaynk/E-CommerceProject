using Business.Apstract;
using Core.Utilities.Results;
using DataAccess.Apstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductReviewsManager:IProductReviewsService
    {
        IProductReviewsDal _productReviewsDal;

        public ProductReviewsManager(IProductReviewsDal productReviewsDal)
        {
            _productReviewsDal = productReviewsDal;
        }

        public IResult Add(ProductComment productReview)
        {
            _productReviewsDal.Add(productReview);
            return new SuccessResult();
 
        }

        public IResult Delete(ProductComment productReview)
        {
            _productReviewsDal.Delete(productReview);
            return new SuccessResult();
        }

        public IDataResult<List<ProductComment>> GetAll()
        {
            return new SuccessDataResult<List<ProductComment>>(_productReviewsDal.GetAll());
        }

        public IResult Update(ProductComment productReview)
        {
            _productReviewsDal.Update(productReview);
            return new SuccessResult();
        }
    }
}
