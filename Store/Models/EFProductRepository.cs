using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace Store.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;
        private IWebHostEnvironment environment;

        public EFProductRepository(ApplicationDbContext ctx, IWebHostEnvironment env)
        {
            context = ctx;
            environment = env;
        }

        public IQueryable<Product> Products => context.Products;

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
                    if (Directory.Exists(environment.ContentRootPath + "\\MyStaticFiles\\Images\\Products\\" + dbEntry.Name) && product.Name != dbEntry.Name)
                    {
                        Directory.Move(environment.ContentRootPath + "\\MyStaticFiles\\Images\\Products\\" + dbEntry.Name, environment.ContentRootPath + "\\MyStaticFiles\\Images\\Products\\" + product.Name);
                        product.ImagePath = "/MyImages/Products/" + product.Name + "/picture.jpg";
                    }
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
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
                if (Directory.Exists(environment.ContentRootPath + "\\MyStaticFiles\\Images\\Products\\" + dbEntry.Name))
                {
                    Directory.Delete(environment.ContentRootPath + "\\MyStaticFiles\\Images\\Products\\" + dbEntry.Name, true);
                }
            }
            return dbEntry;
        }
    }
}
