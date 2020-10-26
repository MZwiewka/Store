using Microsoft.AspNetCore.Mvc;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Store.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products.Select(x => x.Category.Name).Distinct().OrderBy(x => x));
        }
    }
}
