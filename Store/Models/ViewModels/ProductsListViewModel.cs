﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models;

namespace Store.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string SearchedString { get; set; }
        public SortOrder SortOrder { get; set; }
    }

    public enum SortOrder
    {
        Default, Price_ascending, Price_descending
    }
}
