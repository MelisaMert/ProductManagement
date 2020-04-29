using System;
using System.Collections.Generic;
using System.Text;

namespace Abc.Northwind.Entities.Concrete
{
    // Sepet Elamanı Nesnesi
    // Sepet Satırı olarak düşünülebilir.
    // Sepet elemanı olarak bir ürün ve adeti tutulabilir.
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
