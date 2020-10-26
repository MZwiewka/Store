using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Store.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
        public virtual Category ParentCategory { get; set; }
       [JsonIgnore]

        public virtual ICollection<Category> Children { get; set; }

        public Category()
        {
            this.Children = new HashSet<Category>();
        }
    }
}

