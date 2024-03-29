﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models.Attributes
{
    public class IsUnique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var repository = (IProductRepository)validationContext.GetService(typeof(IProductRepository));
            if (!(validationContext.ObjectInstance is Product product)) return new ValidationResult("Model is empty");
            var prod = repository.Products.FirstOrDefault(p => p.Name == (string)value && p.ProductID!= product.ProductID);

            if (prod == null)
                return ValidationResult.Success;
            else
                return new ValidationResult("Product with that name already exists");
        }
    }
}
