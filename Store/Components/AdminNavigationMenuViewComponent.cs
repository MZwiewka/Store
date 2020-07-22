using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Store.Components
{
    public class AdminNavigationMenuViewComponent : ViewComponent
    {
  
        public AdminNavigationMenuViewComponent()
        {
            
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedButton = RouteData?.Values["action"];
            return View();
        }
    }
}