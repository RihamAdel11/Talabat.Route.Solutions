using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Products_Spec;
using Talabat.DTOs;
using Talabat.Errors;

namespace Talabat.Controllers
{
  
    public class ProductsController : BaseApiController 
    {
        
        private readonly IGenericRepositry<Product> _productrepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepositry<Product> Productrepo,IMapper mapper)
        {
         
            _productrepo = Productrepo;
            _mapper = mapper;
        }
        [HttpGet ]
        public async Task<ActionResult <IEnumerable <ProductToReturnDto >>> GetProducts() {
            var spec=new ProductWithBrandandCategorySpecification ();
            var products=await _productrepo .GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable <Product>,IEnumerable <ProductToReturnDto >>(products ));

        }
        [HttpGet("id")]
        public async Task<ActionResult<ProductToReturnDto >> GetProduct(int id)
        {
            var spec = new ProductWithBrandandCategorySpecification(id);
            var product = await _productrepo.GetWithSpecAsync(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));

        }


    }
}
