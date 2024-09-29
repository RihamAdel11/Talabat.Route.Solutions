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
        public ProductWithBrandandCategorySpecification(ProductSpecParams specparams):base(p=>
        (string.IsNullOrEmpty(specparams.Search) || (p.Name.ToLower().Contains(specparams.Search))&&
        (!specparams.BrandId.HasValue ||p.BrandId== specparams.BrandId.Value)&&(!specparams.CategoryId.HasValue ||p.CategoryId== specparams.CategoryId)))
        {
            Includes.Add(p => p.Brand);
            Includes.Add(c => c.Category);
            if (!string.IsNullOrEmpty(specparams.Sort))
            {
                switch (specparams.Sort)
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
            ApplyPagination((specparams.PageIndex - 1) * specparams.pagesize, specparams.pagesize);
        }
        public ProductWithBrandandCategorySpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(c => c.Category);
        }
        
    }
}
