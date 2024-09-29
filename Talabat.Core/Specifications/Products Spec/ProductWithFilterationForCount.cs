﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Products_Spec
{
   public class ProductWithFilterationForCount:BaseSpecifications <Product>
    {
        public ProductWithFilterationForCount(ProductSpecParams specparams) :
            base(p => (string.IsNullOrEmpty(specparams.Search) || (p.Name.ToLower().Contains(specparams.Search))
            && (!specparams.BrandId.HasValue || p.BrandId == specparams.BrandId.Value) && (!specparams.CategoryId.HasValue || p.CategoryId == specparams.CategoryId.Value)))
        {

        }
    }
}
