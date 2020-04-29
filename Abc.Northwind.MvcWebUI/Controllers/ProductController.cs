using Abc.Northwind.Business.Abstract;
using Abc.Northwind.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Northwind.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {   private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }

        public ActionResult Index(int page = 1, int category = 0)
        {
            int pageSize = 10;
            var products = _productService.GetByCategory(category);

            ProductListViewModel model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page
            };
           return View(model);
        }
    }
}
