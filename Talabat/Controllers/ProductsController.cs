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
        private readonly IGenericRepositry<ProductBrand> _BrandRepo;
        private readonly IGenericRepositry<ProductCategory> _CategoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepositry<Product> Productrepo,IGenericRepositry <ProductBrand >BrandRepo,IGenericRepositry <ProductCategory >CategoryRepo,IMapper mapper)
        {
         
            _productrepo = Productrepo;
            _BrandRepo = BrandRepo;
            _CategoryRepo = CategoryRepo;
            _mapper = mapper;
        }
        [HttpGet ]
        [ProducesResponseType(typeof(ProductToReturnDto ),StatusCodes.Status200OK)]
        [ProducesResponseType (typeof(ApiResponse ),StatusCodes .Status404NotFound)]
        public async Task<ActionResult <IEnumerable <ProductToReturnDto >>> GetProducts() {
            var spec=new ProductWithBrandandCategorySpecification ();
            var products=await _productrepo .GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable <Product>,IEnumerable <ProductToReturnDto >>(products ));

        }
        [HttpGet("id")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto >> GetProduct(int id)
        {
            var spec = new ProductWithBrandandCategorySpecification(id);
            var product = await _productrepo.GetWithSpecAsync(spec);
            if (product == null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));

        }
        [HttpGet ("brands")]
        [ProducesResponseType(typeof(ProductBrand), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductBrand >>> GetBrands()
        {
      
            var brands = await _productrepo.GetAllAsync();
            return Ok(brands);

        }
        [HttpGet("categories")]
        [ProducesResponseType(typeof(ProductCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategories()
        {

            var categories = await _productrepo.GetAllAsync();
            return Ok(categories);

        }


    }
}
