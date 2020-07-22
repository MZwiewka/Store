using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Store.Models
{
    public static class Seed
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
            .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "Intel 9900K",
                    Description = "Intel proccessor",
                    Category = "Proccessors",
                    Price = 2200
                },
                new Product
                {
                    Name = "Nvidia RTX 2080ti 11GB",
                    Description = "Nvidia graphics card",
                    Category = "Graphics cards",
                    Price = 5350
                },
                new Product
                {
                    Name = "Western Digital 2TB",
                    Description = "WD 2TB hard disk drive",
                    Category = "Hard drives",
                    Price = 250
                },
                new Product
                {
                    Name = "AMD 5700XT 8GB",
                    Description = "AMD graphics card",
                    Category = "Graphics cards",
                    Price = 1900
                },
                new Product
                {
                    Name = "AMD 3700X",
                    Description = "AMD proccessor",
                    Category = "Proccessors",
                    Price = 1450
                },
                new Product
                {
                    Name = "Nvidia 2070 Super 8GB",
                    Description = "Nvidia graphics card",
                    Category = "Graphics cards",
                    Price = 2400
                },
                new Product
                {
                    Name = "Seagate 1TB",
                    Description = "Seagate 1TB hard disk drive",
                    Category = "Hard drives",
                    Price = 190
                }
                );
                context.SaveChanges();
            }
        }

    }
}
