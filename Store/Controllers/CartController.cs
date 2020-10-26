using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Infrastructure;
using Microsoft.AspNetCore.Http;
using Store.Models.ViewModels;

namespace Store.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });

        }

        public RedirectToActionResult AddToCart(int productID, string returnURL)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productID);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnURL });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
