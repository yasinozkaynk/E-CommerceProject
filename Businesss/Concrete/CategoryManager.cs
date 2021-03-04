﻿
using Business.Apstract;
using Core.Utilities.Results;
using DataAccess.Apstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<List<Category>> GetById(int categoryId)
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(c => c.CategoryId == categoryId));
        }
    }
}
