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
        public ProductWithBrandandCategorySpecification():base()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(c => c.Category);

        }
        public ProductWithBrandandCategorySpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(c => c.Category);
        }
        
    }
}
