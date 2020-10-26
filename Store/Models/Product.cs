using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Store.Models.Attributes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Store.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        [IsUnique]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        [ValidateNever]
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public virtual List <SpecificationFieldValue> Specification { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        public Product()
        {
            this.Specification = new List<SpecificationFieldValue>();
        }
    }
}