using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public EFProductRepository(ApplicationDbContext ctx, IWebHostEnvironment env)
        {
            context = ctx;
            environment = env;
        }

        public IQueryable<Product> Products => context.Products;

        public IQueryable<Category> Categories => context.Categories;

        public IQueryable<SpecificationField> SpecificationFields => context.SpecificationFields;

        public IQueryable<SpecificationFieldValue> SpecificationFieldValues => context.SpecificationFieldValues;

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {           
                    if (Directory.Exists($"{environment.ContentRootPath}\\MyStaticFiles\\Images\\Products\\{dbEntry.Name}") && product.Name != dbEntry.Name)
                    {
                        Directory.Move($"{environment.ContentRootPath}\\MyStaticFiles\\Images\\Products\\{dbEntry.Name}", $"{environment.ContentRootPath}\\MyStaticFiles\\Images\\Products\\{product.Name}");
                        product.ImagePath = $"/MyImages/Products/{product.Name}/picture.jpg";
                    }
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.CategoryID = product.CategoryID;
                    var result = product.Specification.Where(x => !dbEntry.Specification.Any(y => y.SpecificationField.Name == x.SpecificationField.Name)).ToList();
                    if (result.Count == 0)
                    {
                        result = dbEntry.Specification.Concat(result).ToList();
                        result.Remove(result.Last());

                    }
                    else 
                    {
                        result = dbEntry.Specification.Concat(result).ToList();
                    }
                    dbEntry.Specification = result;
                    dbEntry.ImagePath = product.ImagePath;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products
            .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
                if (Directory.Exists($"{environment.ContentRootPath}\\MyStaticFiles\\Images\\Products\\{dbEntry.Name}"))
                {
                    Directory.Delete($"{environment.ContentRootPath}\\MyStaticFiles\\Images\\Products\\{dbEntry.Name}", true);
                }
            }
            return dbEntry;
        }

        public void SaveSpecificationCategory(SpecificationField specificationField, SpecificationFieldValue specificationFieldValue)
        {
            if (specificationField.SpecificationFieldID == 0 && specificationFieldValue.SpecificationFieldValueID == 0)
            {
                context.SpecificationFields.Add(specificationField);
                context.SpecificationFieldValues.Add(specificationFieldValue);
            }
            else
            {
               
            }
            context.SaveChanges();
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryID == 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                Category dbEntry = context.Categories
                    .FirstOrDefault(c => c.CategoryID == category.CategoryID);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                    dbEntry.ParentCategoryID = category.ParentCategoryID;
                }
            }
            context.SaveChanges();
        }

        public Category DeleteCategory(int categoryID)
        {
            Category dbEntry = context.Categories.FirstOrDefault(c => c.CategoryID == categoryID);
            if (dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
