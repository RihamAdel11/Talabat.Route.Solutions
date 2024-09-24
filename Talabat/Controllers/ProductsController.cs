using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Products_Spec;

namespace Talabat.Controllers
{
  
    public class ProductsController : BaseApiController 
    {
        
        private readonly IGenericRepositry<Product> _productrepo;

        public ProductsController(IGenericRepositry<Product> Productrepo)
        {
         
            _productrepo = Productrepo;
        }
        [HttpGet ]
        public async Task<ActionResult <IEnumerable <Product>>> GetProducts() {
            var spec=new ProductWithBrandandCategorySpecification ();
            var products=await _productrepo .GetAllWithSpecAsync(spec);
            return Ok(products);

        }
        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandandCategorySpecification(id);
            var product = await _productrepo.GetWithSpecAsync(spec);
            return Ok(product);

        }


    }
}
