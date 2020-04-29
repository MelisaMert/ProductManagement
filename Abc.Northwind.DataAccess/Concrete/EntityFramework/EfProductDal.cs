using Abc.Core.DataAccess.EntityFramework;
using Abc.Northwind.DataAccess.Abstact;
using Abc.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product,NorthwindContext>, IProductDal
    { 

    }
}
