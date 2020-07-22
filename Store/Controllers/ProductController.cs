using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 3;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int productPage = 1) 
            => View(new ProductsListViewModel { 
                Products = repository.Products
                .Where(p=> category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize), 
                PagingInfo = new PagingInfo { 
                    CurrentPage = productPage, 
                    ItemsPerPage = PageSize, 
                    TotalItems = category == null ? 
                        repository.Products.Count() : 
                        repository.Products.Where(e => 
                            e.Category == category).Count()
                }, 
                CurrentCategory = category,
                SearchedString = null
            });

        public ViewResult Info(int productID) =>
            View(repository.Products.FirstOrDefault(p => p.ProductID == productID));

        public ViewResult Search(string searchString, int productPage = 1)
           => View(new ProductsListViewModel
           {
               Products = repository.Products
               .Where(p => p.Name.Contains(searchString))
               .OrderBy(p => p.ProductID)
               .Skip((productPage - 1) * PageSize)
               .Take(PageSize),
               PagingInfo = new PagingInfo
               {
                   CurrentPage = productPage,
                   ItemsPerPage = PageSize,
                   TotalItems = 
                       repository.Products.Where(p => p.Name.Contains(searchString)).Count()
               },
               CurrentCategory = null,
               SearchedString = searchString
           });
    }
}