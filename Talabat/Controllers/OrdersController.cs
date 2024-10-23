using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Service.Contract;
using Talabat.DTOs;
using Talabat.Errors;

namespace Talabat.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderservices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderservices, IMapper mapper)
        {
            _orderservices = orderservices;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderservices.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, address, orderDto.DeliveryMethodId);
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }
        [HttpGet]
        
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string email)
        {
            var orders = await _orderservices.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id, string email)
        {
            var order = await _orderservices.GetOrderByIdForUserAsync(id, email);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }
        
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethod = await _orderservices.GetDeliveryMethodAsync();
            return Ok(deliveryMethod);
        }
    }
}
