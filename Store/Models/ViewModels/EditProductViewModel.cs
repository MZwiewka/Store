using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }

        public SelectList Categories { get; set; }

    }
}
