using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Northwind.Business.Abstract;
using Abc.Northwind.Entities.Concrete;
using Abc.Northwind.MvcWebUI.Models;
using Abc.Northwind.MvcWebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Northwind.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        private IProductService _productService;
        public CartController(ICartSessionService cartSessionService,
                              ICartService cartService,
                              IProductService productService)
        {
            _cartSessionService = cartSessionService;
            _cartService = cartService;
            _productService = productService;
        }

        public ActionResult AddToCart(int productId)
        {
            var productToBeAdded = _productService.GetById(productId);
            var cart = _cartSessionService.GetCart(); // Sepet Session 'ı verir. Cart nesnesi var.
            _cartService.AddToCart(cart, productToBeAdded);
            _cartSessionService.SetCart(cart);
            TempData.Add("message",String.Format("Your product ,{0}, was successfully added to the cart!", productToBeAdded.ProductName)); // Temp Data tek bir requestlik veri taşır.
            return RedirectToAction("Index", "Product");
        }
        public ActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            CartSummaryViewModel cartSummaryViewModel = new CartSummaryViewModel
            {
                Cart = cart
            };
            return View(cartSummaryViewModel);
        }
        public ActionResult Remove(int productId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", String.Format("Your product was successfully removed to the cart!")); // Temp Data tek bir requestlik veri taşır.
            return RedirectToAction("List");
        }
        public ActionResult Complete()
        {
            var shippingDetailsViewModel = new ShippingDetailsViewModel()
            {
                 ShippingDetails = new ShippingDetails()
            };
            return View(shippingDetailsViewModel);
        }
        
        [HttpPost]
        public ActionResult Complete(ShippingDetails shippingDetails)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", String.Format("Thank you {0}, you order is in process", shippingDetails.FirstName));
            return View();
        }
    }
}