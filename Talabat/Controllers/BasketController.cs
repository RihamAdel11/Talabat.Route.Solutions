using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.DTOs;
using Talabat.Errors;

namespace Talabat.Controllers
{
    public class BasketController : BaseApiController 
    {
        private readonly IBasketRepository  _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket (id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdated = await _basketRepo.UpdateBasketAsync (mappedBasket);
            if (createdOrUpdated is null) return BadRequest(new ApiResponse (400));
            return Ok(createdOrUpdated);
        }
        [HttpDelete]
        public async Task DeletBasket(string id)
        {
            await _basketRepo.DeleteBasketAsync(id);
        }
    }
}

    

