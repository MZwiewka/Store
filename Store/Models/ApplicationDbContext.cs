using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set;  }
        public DbSet<Category> Categories { get; set; }

        public DbSet<SpecificationField> SpecificationFields { get; set; }
        public DbSet<SpecificationFieldValue> SpecificationFieldValues { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Category>(category => {
                category.HasMany(c => c.Children)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryID);
            });
        }
    }
}
