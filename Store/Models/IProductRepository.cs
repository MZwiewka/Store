using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<SpecificationField> SpecificationFields { get; }
        IQueryable<SpecificationFieldValue> SpecificationFieldValues { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);

        void SaveCategory(Category category);

        Category DeleteCategory(int categoryID);
    }
}
