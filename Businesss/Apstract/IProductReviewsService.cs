using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Apstract
{
    public interface IProductReviewsService
    {
        IResult Add(ProductComment productReview);
        IResult Delete(ProductComment productReview);
        IResult Update(ProductComment productReview);
        IDataResult<List<ProductComment>> GetAll();
    }
}
