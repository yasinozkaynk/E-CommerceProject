﻿using Core.DataAccess.EntityFramework;
using DataAccess.Apstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductImageDal :EfEntityRepositoryBase<ProductImagePath,NorthwindContext>,IProductImageDal
    {

    }
}
