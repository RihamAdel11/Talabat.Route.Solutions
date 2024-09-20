using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

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
            var products=await _productrepo .GetAllAsync();
            return Ok(products);

        }
        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productrepo.GetAsync(id);
            return Ok(product);

        }


    }
}
