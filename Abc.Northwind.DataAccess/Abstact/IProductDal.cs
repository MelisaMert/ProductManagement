﻿using Abc.Core.DataAccess;
using Abc.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Northwind.DataAccess.Abstact
{
    public interface IProductDal : IEntityRepository<Product>
    {
    }
}
