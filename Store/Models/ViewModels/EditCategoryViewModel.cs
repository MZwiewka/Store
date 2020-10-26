using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.Models.ViewModels
{
    public class EditCategoryViewModel
    {
       
        public Category Category { get; set; }

        public SelectList Categories { get; set; }

        public EditCategoryViewModel(Category category, SelectList categories)
        {
            Category = category;
            Categories = categories;
        }
    }
}
