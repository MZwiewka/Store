﻿using System;
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

        public ViewResult List(string category, string searchString, string sortOrder = "Default", int productPage = 1)
        {
            var products = repository.Products
                  .Where(p => category == null || p.Category == category);

            if(!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Price descending":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "Price ascending":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "Default":
                    products = products.OrderBy(p => p.ProductID);
                    break;
            }

            return View(new ProductsListViewModel
            {
                Products = products.Skip((productPage - 1) * PageSize)
                  .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = products.Count()
                },
                CurrentCategory = category,
                SearchedString = searchString,
                SortOrder = sortOrder
            });
        }

        public ViewResult Info(int productID) =>
            View(repository.Products.FirstOrDefault(p => p.ProductID == productID));
    }
}