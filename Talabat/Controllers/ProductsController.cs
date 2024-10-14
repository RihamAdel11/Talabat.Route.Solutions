using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Products_Spec;
using Talabat.DTOs;
using Talabat.Errors;
using Talabat.Helpers;

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
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Paginations<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productspecparams) {
            var spec = new ProductWithBrandandCategorySpecification(productspecparams);
            var products = await _productrepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countspec = new ProductWithBrandandCategorySpecification(productspecparams);
            var count = await _productrepo.GetCountAsync(countspec); 

            return Ok(new Paginations <ProductToReturnDto >(productspecparams.PageIndex ,productspecparams.pagesize,count,data));

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
        public async Task<ActionResult<IReadOnlyList<ProductBrand >>> GetBrands()
        {
      
            var brands = await _productrepo.GetAllAsync();
            return Ok(brands);

        }
        [HttpGet("categories")]
        [ProducesResponseType(typeof(ProductCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {

            var categories = await _productrepo.GetAllAsync();
            return Ok(categories);

        }


    }
}
