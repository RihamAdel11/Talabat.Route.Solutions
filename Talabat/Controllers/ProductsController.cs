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



    }
}
