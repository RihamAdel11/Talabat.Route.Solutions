using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Products_Spec
{
   public class ProductWithBrandandCategorySpecification:BaseSpecifications <Product>
    {
        public ProductWithBrandandCategorySpecification(string? sort,int?brandId,int?categoryId):base(p=>
        (!brandId.HasValue ||p.BrandId==brandId.Value)&&(!categoryId .HasValue ||p.CategoryId==categoryId ))
        {
            Includes.Add(p => p.Brand);
            Includes.Add(c => c.Category);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price); break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
                AddOrderBy(p => p.Name);
        }
        public ProductWithBrandandCategorySpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(c => c.Category);
        }
        
    }
}
