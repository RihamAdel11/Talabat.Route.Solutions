using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.Controllers
{
   
    public class BuggyController : BaseApiController 
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbcontext.Products.Find(100);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("ServerError")]
        public ActionResult GetServerErrorRequest()
        {
            var product = _dbcontext.Products.Find(100);
            var productToReturn = product.ToString();
            return Ok(product);
        }
        [HttpGet("BadRequest")]
        public IActionResult GetbadRequestRequest()
        {


            return BadRequest();
        }
        [HttpGet("BadRequest/{id}")]//Validation Error
        public IActionResult GetbadRequestRequest(int id)
        {


            return Ok();
        }
        [HttpGet("UnAuthorixedError")]
        public IActionResult GetUnAuthorixedErrorRequest()
        {


            return Unauthorized();
        }

    }
}
