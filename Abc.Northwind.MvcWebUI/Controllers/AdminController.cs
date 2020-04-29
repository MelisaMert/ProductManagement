using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Northwind.Business.Abstract;
using Abc.Northwind.Entities.Concrete;
using Abc.Northwind.MvcWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Northwind.MvcWebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // CRUD + ASP.NET IDENTITY MVC FRAMEWORK
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public ActionResult Index()
        {
            var productListViewModel = new ProductListViewModel()
            {
                Products = _productService.GetAll()
            };
            return View(productListViewModel);
        }
        // Get Operation
        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(), // ürün özellikleri vs kullanılmak isternirse
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Product product)
        {
            if(ModelState.IsValid)
            {
                _productService.Add(product);
                TempData.Add("message", "Product was successfully added");
            }
            return RedirectToAction("Add"); // Direkt view 'i açma aksiyonu çalıştır
        }
        // Get Operation
        public ActionResult Update(int productId)
        {
            var model = new ProductUpdateViewModel
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Update(product);
                TempData.Add("message", "Product was successfully updated");
            }
            return RedirectToAction("Update"); // Direkt view 'i açma aksiyonu çalıştır
        }
        public ActionResult Delete(int productId)
        {
            _productService.Delete(productId);
            TempData.Add("message", "Product was successfully deleted");
            return RedirectToAction("index");
         
        }
    }
}