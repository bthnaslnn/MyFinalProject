﻿using Core.DateAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{

    //interface metotları default olarak publicdir.

    public interface IProductDal :IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();
    }
}


//Code Refactoring